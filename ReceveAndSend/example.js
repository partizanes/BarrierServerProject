var activex = new ActiveXObject("WScript.Shell");
activex.Run("ReceveAndSend.exe 192.168.1.191 7000 5010 192.168.1.178 7000 5010 1", 1, true);