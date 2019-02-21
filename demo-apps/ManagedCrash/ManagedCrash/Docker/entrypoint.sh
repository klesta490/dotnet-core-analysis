#!/bin/bash
set -e
#echo '/tmp/core.%h.%e.%t' > /proc/sys/kernel/core_pattern
ulimit -c unlimited
#echo 0xff > /proc/self/coredump_filter
dotnet /app/ManagedCrash.dll $@
