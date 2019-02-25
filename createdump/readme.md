# How to create dumps

### Docker
You need to run docker image as `--privileged` or `--cap-add=SYS_PTRACE`

### Createdump utility

Can be found in .net core installation directory
e.g.
```
/usr/share/dotnet/shared/Microsoft.NETCore.App/2.2.2/createdump
```
#### Create on demand
```
/usr/share/dotnet/shared/Microsoft.NETCore.App/2.2.2/createdump -u -p 1
```

#### Create on crash
```
COMPlus_DbgEnableMiniDump: if set to "1", enables this core dump generation. The default is NOT to generate a dump.
COMPlus_DbgMiniDumpType: if set to "1" generates MiniDumpNormal, "2" MiniDumpWithPrivateReadWriteMemory, "3" MiniDumpFilterTriage, "4" MiniDumpWithFullMemory. Default is MiniDumpNormal.
COMPlus_DbgMiniDumpName: if set, use as the template to create the dump path and file name. The pid can be placed in the name with %d. The default is /tmp/coredump.%d.
COMPlus_CreateDumpDiagnostics: if set to "1", enables the createdump utilities diagnostic messages (TRACE macro).
```


### Core dumps
```
echo '/tmp/core.%h.%e.%t' > /proc/sys/kernel/core_pattern
ulimit -c unlimited
echo 0xff > /proc/self/coredump_filter
```



# How to analyze dump
**EXACTLY Same lib are needed, currently not possible to open dump on other environment**

### What to do with crashed docker container?
```
docker ps -a
docker commit -m "coredump" 92b8935a7cd7
docker run -it --entrypoint /bin/bash <created image ID> 

```

### LLDB >= 3.9

Run docker as `--privileged` so `/proc/sys/kernel/core_pattern` is accessible

```
find /usr/share/dotnet -name libsosplugin.so
lldb-4.0 $(which dotnet) --core /tmp/coredump.1
thread list
thread backtrace
plugin load /usr/share/dotnet/shared/Microsoft.NETCore.App/2.2.2/libsosplugin.so
setclrpath /usr/share/dotnet/shared/Microsoft.NETCore.App/2.2.2
sos Thread
sos DumpStack
sos EESStack
```

### Symbols
[Dotnet symbol](https://github.com/dotnet/symstore/tree/master/src/dotnet-symbol#install)
```
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

dotnet tool install -g dotnet-symbol

dotnet symbol coredump.4507
```

[Createdump](https://github.com/dotnet/coreclr/blob/master/Documentation/botr/xplat-minidump-generation.md)

[Sasha Goldstein](http://blogs.microsoft.co.il/sasha/2017/02/26/analyzing-a-net-core-core-dump-on-linux/)