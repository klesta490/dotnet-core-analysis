# perf - [Man](http://man7.org/linux/man-pages/man1/perf.1.html)

## Installation

**Ubutnu**
```
apt-get install linux-tools linux-tools-generic  linux-tools-common
```

**Debian**

```
apt-get install linux-perf
```

**Centos**
```
yum install perf
```



## Run image

```
dotnet-core-analysis/demo-apps/PerfProblem/PerfProblem/Docker/build-docker-image.bat
dotnet-core-analysis/demo-apps/PerfProblem/PerfProblem/Docker/push-docker-image.bat
```

```
docker run -d -e COMPlus_PerfMapEnabled=1 <imagename>
docker container ls
docker top <containername>
```

## Measure
```
perf record -a -g -F 97 -p <PID>
```

## Analyze results
```
perf report -f
perf report -T
git clone --depth=1 https://github.com/BrendanGregg/FlameGraph
perf script | FlameGraph/stackcollapse-perf.pl | FlameGraph/flamegraph.pl > flame.svg
```

```
perf record 
-a   all cpus
-g stacks  - needed for flamegraphs
```






**Centos**
docker cp azurerunner_test:/tmp/perf-1.map /tmp/perf-10142.map


http://www.brendangregg.com/




credits: [Sasha Goldstein](http://blogs.microsoft.co.il/sasha/2017/02/27/profiling-a-net-core-application-on-linux/)
