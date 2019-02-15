COMPlus_PerfMapEnabled=1




https://github.com/dotnet/coreclr/blob/master/Documentation/botr/xplat-minidump-generation.md

http://blogs.microsoft.co.il/sasha/2017/02/26/analyzing-a-net-core-core-dump-on-linux/

http://blogs.microsoft.co.il/sasha/2017/02/27/profiling-a-net-core-application-on-linux/

http://blogs.microsoft.co.il/sasha/2017/03/30/tracing-runtime-events-in-net-core-on-linux/

https://blogs.microsoft.co.il/sasha/2018/02/06/getting-stacks-for-lttng-events-with-net-core-on-linux/


Dump on crash
------------------
journalctl


// jeste nevyzkouseno   !!!! MIT DOST MISTA NA DISKU
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

2) 
pustit docker run jako --privileged   aby mel pristup do /proc/sys/kernel/core_pattern 

3) po padu 

docker ps -a

docker commit -m "coredump" 92b8935a7cd7

docker commit -m "coredump" 92b8935a7cd7

docker run -it --entrypoint /bin/bash <created image ID> 



4) uvnitr image
find /usr/share/dotnet -name libsosplugin.so
/usr/share/dotnet/shared/Microsoft.NETCore.App/1.1.0/libsosplugin.so


lldb-4.0 $(which dotnet) --core /tmp/coredump.1
plugin load /usr/share/dotnet/shared/Microsoft.NETCore.App/2.1.7/libsosplugin.so
setclrpath /usr/share/dotnet/shared/Microsoft.NETCore.App/2.1.7

/usr/share/dotnet/shared/Microsoft.NETCore.App/2.1.7
/usr/share/dotnet/shared/Microsoft.NETCore.App/2.1.7/libsosplugin.so


nestaci se jenom execnout jako --privileged, stejne create dump napise ze name prava, je potreba  aby to bylo pusteny jako privileged nebo SYS_PTRACE




(lldb) sos Threads
Failed to load data access DLL, 0x80004005
Can not load or initialize libmscordaccore.so. The target runtime may not be initialized.
Threads  failed
