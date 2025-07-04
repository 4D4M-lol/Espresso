// Imports

using System.Collections.Concurrent;

// Script Namespace

namespace Espresso.EspScript
{
    // Classes

    public class EsSignal<TCallback> where TCallback : Delegate
    {
        // Properties and Fields
        
        private readonly ConcurrentDictionary<string, (TCallback c, bool o)> _callbacks;
        private readonly object _lockObject = new();

        public Dictionary<string, (TCallback Callback, bool Once)> Callbacks
        {
            get
            {
                lock (_lockObject)
                {
                    return _callbacks.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                }
            }
        }
        
        // Constructors and Methods

        public EsSignal()
        {
            _callbacks = new();
        }

        public void Connect(string id, TCallback callback, bool overwrite = false)
        {
            lock (_lockObject)
            {
                if (!overwrite && _callbacks.ContainsKey(id))
                {
                    return;
                }

                _callbacks.AddOrUpdate(id, (callback, false), (_, _) => (callback, false));
            }
        }

        public void Once(string id, TCallback callback, bool overwrite = false)
        {
            lock (_lockObject)
            {
                if (!overwrite && _callbacks.ContainsKey(id))
                {
                    return;
                }

                _callbacks.AddOrUpdate(id, (callback, true), (_, _) => (callback, true));
            }
        }

        public void Disconnect(string id)
        {
            lock (_lockObject)
            {
                _callbacks.TryRemove(id, out _);
            }
        }

        public bool Has(string id)
        {
            return _callbacks.ContainsKey(id);
        }

        public void Emit(params object[] args)
        {
            List<(string id, TCallback callback, bool once)> callbacksToExecute;
            
            lock (_lockObject)
            {
                callbacksToExecute = _callbacks.Select(kvp => (kvp.Key, kvp.Value.c, kvp.Value.o)).ToList();
            }

            List<string> onceCallbacksToRemove = new();
            
            foreach (var (id, callback, once) in callbacksToExecute)
            {
                try
                {
                    if (args.Length == 0)
                    {
                        callback.DynamicInvoke();
                    }
                    else
                    {
                        callback.DynamicInvoke(args);
                    }

                    if (once)
                    {
                        onceCallbacksToRemove.Add(id);
                    }
                }
                catch (Exception e)
                {
                    if (EsConfigs.Log)
                    {
                        Console.WriteLine($"Espresso: Error while emitting callback #{id}: {e.Message}");
                    }
                }
            }

            foreach (string id in onceCallbacksToRemove)
            {
                Disconnect(id);
            }
        }

        public async Task EmitAsync(params object[] args)
        {
            List<(string id, TCallback callback, bool once)> callbacksToExecute;
            
            lock (_lockObject)
            {
                callbacksToExecute = _callbacks.Select(kvp => (kvp.Key, kvp.Value.c, kvp.Value.o)).ToList();
            }

            List<Task> tasks = new();
            List<string> onceCallbacksToRemove = new();

            foreach (var (id, callback, once) in callbacksToExecute)
            {
                Task task = Task.Run(() =>
                {
                    try
                    {
                        if (args.Length == 0)
                        {
                            callback.DynamicInvoke();
                        }
                        else
                        {
                            callback.DynamicInvoke(args);
                        }

                        if (once)
                        {
                            lock (_lockObject)
                            {
                                onceCallbacksToRemove.Add(id);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (EsConfigs.Log)
                        {
                            Console.WriteLine($"Espresso: Error while emitting callback #{id}: {e.Message}");
                        }
                    }
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            foreach (string id in onceCallbacksToRemove)
            {
                Disconnect(id);
            }
        }

        public void Dispose()
        {
            _callbacks.Clear();
            GC.SuppressFinalize(this);
        }

        public override string ToString()
        {
            return "[EsSignal]";
        }
    }
}