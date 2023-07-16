#!/bin/bash

# .net core 项目 Linux 安装脚本

user=`whoami`
if [ "$user" != "root" ]; then
    echo "[+] restart with sudo ..."
    sudo $0
    exit 1
fi

# info
# serverName="$1"
serverName="SuyaaProxy"
serverProgram="Suyaa.Proxy.Host"
serverFile="/etc/systemd/system/$serverName.service"
serverDec="Cloud Database Server"
execFolder=`pwd`
execFile="$execFolder/$serverProgram"

echo "[+] stop service ..."
systemctl stop $serverName

echo "[+] disable service ..."
systemctl disable $serverName

echo "[+] delete file $serverFile ..."
rm -rf $serverFile

echo "[+] reload services ..."
systemctl daemon-reload

