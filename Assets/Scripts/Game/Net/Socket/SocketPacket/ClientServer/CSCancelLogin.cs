﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class CSCancelLogin : SocketPacket
{
	public CSCancelLogin(PACKET_TYPE type)
		: base(type) { }
	protected override void fillParams(){}
}