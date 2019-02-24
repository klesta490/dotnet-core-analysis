Ubutnu 
apt-get install linux-tools linux-tools-generic  (linux tools common)


COMPlus_PerfMapEnabled=1

https://github.com/dotnet/coreclr/blob/master/Documentation/botr/xplat-minidump-generation.md

http://blogs.microsoft.co.il/sasha/2017/02/26/analyzing-a-net-core-core-dump-on-linux/

http://blogs.microsoft.co.il/sasha/2017/02/27/profiling-a-net-core-application-on-linux/

http://blogs.microsoft.co.il/sasha/2017/03/30/tracing-runtime-events-in-net-core-on-linux/

https://blogs.microsoft.co.il/sasha/2018/02/06/getting-stacks-for-lttng-events-with-net-core-on-linux/


Dump on crash
------------------
journalctl


COMPlus_DbgEnableMiniDump: if set to "1", enables this core dump generation. The default is NOT to generate a dump.
COMPlus_DbgMiniDumpType: if set to "1" generates MiniDumpNormal, "2" MiniDumpWithPrivateReadWriteMemory, "3" MiniDumpFilterTriage, "4" MiniDumpWithFullMemory. Default is MiniDumpNormal.
COMPlus_DbgMiniDumpName: if set, use as the template to create the dump path and file name. The pid can be placed in the name with %d. The default is /tmp/coredump.%d.
COMPlus_CreateDumpDiagnostics: if set to "1", enables the createdump utilities diagnostic messages (TRACE macro).


// vyzkouseno 


https://dev.to/mizutani/how-to-get-core-file-of-segmentation-fault-process-in-docker-22ii


upravit docker aby poustel entrypoint.sh
v nem 

1) 
echo '/tmp/core.%h.%e.%t' > /proc/sys/kernel/core_pattern
ulimit -c unlimited
echo 0xff > /proc/self/coredump_filter

2) 
pustit docker run jako --privileged   aby mel pristup do /proc/sys/kernel/core_pattern 

3) po padu 

docker ps -a

docker commit -m "coredump" 92b8935a7cd7

docker run -it --entrypoint /bin/bash <created image ID> 



4) uvnitr image
find /usr/share/dotnet -name libsosplugin.so
/usr/share/dotnet/shared/Microsoft.NETCore.App/1.1.0/libsosplugin.so


lldb-4.0 $(which dotnet) --core /tmp/coredump.1
plugin load /usr/share/dotnet/shared/Microsoft.NETCore.App/2.2.2/libsosplugin.so
plugin load /usr/share/dotnet/shared/Microsoft.NETCore.App/2.1.7/libsosplugin.so
setclrpath /usr/share/dotnet/shared/Microsoft.NETCore.App/2.2.2
sethostruntime /usr/share/dotnet/shared/Microsoft.NETCore.App/2.1.7

/usr/share/dotnet/shared/Microsoft.NETCore.App/2.1.7
/usr/share/dotnet/shared/Microsoft.NETCore.App/2.1.7/libsosplugin.so


nestaci se jenom execnout jako --privileged, stejne create dump napise ze name prava, je potreba  aby to bylo pusteny jako privileged nebo SYS_PTRACE




(lldb) sos Threads
Failed to load data access DLL, 0x80004005
Can not load or initialize libmscordaccore.so. The target runtime may not be initialized.
Threads  failed




COMPlus_PerfMapEnabled=1


perf record -a -g -F 97 -p <PID>
perf report -f
docker cp azurerunner_test:/tmp/perf-1.map /tmp/perf-10142.map

http://www.brendangregg.com/
git clone --depth=1 https://github.com/BrendanGregg/FlameGraph


perf script | FlameGraph/stackcollapse-perf.pl | FlameGraph/flamegraph.pl > flame.svg



https://github.com/dotnet/coreclr/blob/master/Documentation/botr/xplat-minidump-generation.md
https://github.com/dotnet/symstore/tree/master/src/dotnet-symbol#install



Debian
perf
-----
apt-get install linux-perf

sdk 2.1
----
apt install wget
apt install gpg
apt-get install apt-transport-https


wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg
sudo mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/
wget -q https://packages.microsoft.com/config/debian/9/prod.list
sudo mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
sudo chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg
sudo chown root:root /etc/apt/sources.list.d/microsoft-prod.list


sudo apt-get update
sudo apt-get install dotnet-sdk-2.1
exec bash -l



LTTNG
Exceptions (lttng)
---------------------
https://github.com/dotnet/corefx-tools/blob/master/src/performance/perfcollect/perfcollect
https://github.com/dotnet/coreclr/blob/master/Documentation/project-docs/linux-performance-tracing.md


curl -OL http://aka.ms/perfcollect
chmod +x perfcollect
sudo ./perfcollect install


lltng 
COMPlus_EnableEventLog=1


# lttng create exceptions-trace
lttng add-context --userspace --type vpid
lttng add-context --userspace --type vtid
lttng add-context --userspace --type procname
lttng enable-event --userspace --tracepoint DotNETRuntime:Exception*
lttng start
lttng stop
lttng destroy
babeltrace ~/exceptions-trace









__vdso_clock_gettime
https://blog.packagecloud.io/eng/2017/03/08/system-calls-are-much-slower-on-ec2/