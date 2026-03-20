#! /usr/bin/env bash
set -uvx
set -e
cd "$(dirname "$0")"
cwd=`pwd`
ts=`date "+%Y.%m%d.%H%M.%S"`
cd UniversalEncoding.Demo
dotnet run --project UniversalEncoding.Demo.csproj --framework net462 "$@"
