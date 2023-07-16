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
serverProgram="Suyaa.Proxy.Remote.Host"
serverFile="/etc/systemd/system/$serverName.service"
serverDec="$serverName Server"
execFolder=`pwd`
execFile="$execFolder/$serverProgram"

echo "[+] execute file $execFile ..."
echo "[+] create file $serverFile ..."

echo '[Unit]' > $serverFile
echo "Description=$serverDec" >> $serverFile
echo '' >> $serverFile
echo '[Service]' >> $serverFile
echo 'Type=simple' >> $serverFile
echo "WorkingDirectory=$execFolder" >> $serverFile
#Environment=ASPNETCORE_ENVIRONMENT=Production
#echo 'Environment=ASPNETCORE_ENVIRONMENT=Development' >> $serverFile
echo "ExecStart=$execFile" >> $serverFile
echo 'ExecStop=/bin/kill -2 $MAINPID' >> $serverFile
echo 'KillMode=process' >> $serverFile
echo 'Restart=on-failure' >> $serverFile
echo 'RestartSec=1s' >> $serverFile
echo '' >> $serverFile
echo '[Install]' >> $serverFile
echo 'WantedBy=multi-user.target' >> $serverFile

echo "[+] reload services ..."
systemctl daemon-reload

echo "[+] enable service ..."
systemctl enable $serverName

echo "[+] start service ..."
systemctl start $serverName