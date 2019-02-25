LTTNG (lttng)
---------------------
[perfcollect src](https://github.com/dotnet/corefx-tools/blob/master/src/performance/perfcollect/perfcollect)

[perfcollect doc](https://github.com/dotnet/coreclr/blob/master/Documentation/project-docs/linux-performance-tracing.md)

```
curl -OL http://aka.ms/perfcollect
chmod +x perfcollect
sudo ./perfcollect install
```

```
COMPlus_EnableEventLog=1
```

```
# lttng create exceptions-trace
lttng add-context --userspace --type vpid
lttng add-context --userspace --type vtid
lttng add-context --userspace --type procname
lttng enable-event --userspace --tracepoint DotNETRuntime:Exception*
lttng start
lttng stop
lttng destroy
babeltrace ~/exceptions-trace
```


[Sasha Goldstein 1](http://blogs.microsoft.co.il/sasha/2017/03/30/tracing-runtime-events-in-net-core-on-linux/)

[Sasha Goldstein 2](https://blogs.microsoft.co.il/sasha/2018/02/06/getting-stacks-for-lttng-events-with-net-core-on-linux/)
