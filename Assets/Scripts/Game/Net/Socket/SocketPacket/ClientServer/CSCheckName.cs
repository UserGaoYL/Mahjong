﻿using System;
using System.Collections;
using System.Collections.Generic;

public class CSCheckName : SocketPacket
{
	public BYTES mName = new BYTES(16);
	public CSCheckName(PACKET_TYPE type)
		: base(type) { }
	public void setName(byte[] name)
	{
		mName.setValue(name);
	}
	protected override void fillParams()
	{
		pushParam(mName);
	}
}