#!/bin/sh

function die {
	echo "Install Failed!"
	if [ $1 ]
	then
		echo "Reason: $1"
	fi
	exit
}

#Yeah, it's really that simple

INSTALL='install -m 0755 -o root -g root'
INSTALLDIR='install -o root -g root -d'
LIB_DIR='/usr/lib'
BIN_DIR='/usr/bin'

if [ "$UID" != "0" ]
then
	echo "Must be root to install"
	exit 101
fi

tar xf bin/wxNET.tar -C $LIB_DIR

$INSTALL bin/Absinthe.exe $BIN_DIR || die
$INSTALL bin/Absinthe.Core.dll $BIN_DIR || die
$INSTALL bin/Absinthe.ico $BIN_DIR || die
$INSTALL bin/runabsinthe.sh $BIN_DIR || die
$INSTALLDIR $BIN_DIR/plugins || die
$INSTALL bin/plugins/Absinthe.Plugins.dll $BIN_DIR/plugins || die
ln -sf $BIN_DIR/runabsinthe.sh $BIN_DIR/absinthe || die

if [ $LIB_DIR != "/usr/lib" ] && [ $LIB_DIR != "/lib" ]
then
		grep ^$LIB_DIR$ /etc/ld.so.conf > /dev/null || echo $LIB_DIR >> /etc/ld.so.conf && echo "Adding entry to ld.so.conf for $LIB_DIR" || die
fi

/sbin/ldconfig || die "ldconfig failed"

echo absinthe has been installed to $BIN_DIR
echo to run, simply type: absinthe
echo \(You will need to have at least Mono v1.0 to run Absinthe\)
