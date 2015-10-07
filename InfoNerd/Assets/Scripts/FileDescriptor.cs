using UnityEngine;
using System.Collections;
using System.Xml;

public class FileDescriptor {

	public string _title = "";
	public string _pathOnline = "";
	public string _pathLocal = "";

	public bool _online = true;
	public bool _loaded = false;

	private XmlDocument _xmlRootDoc;

	private XmlNode _pathOnlineNode;
	private XmlNode _pathLocalNode;
	private XmlNode _loadedNode;

	private MainController _mc;

	public FileDescriptor(XmlDocument xmlRoot, MainController mc){
		_xmlRootDoc = xmlRoot;
		_mc = mc;
	}

	public FileDescriptor(string title, string pathOnline, bool loaded, bool online){
		_title = title;
		_pathOnline = pathOnline;
		_loaded = loaded;
		_online = online;
	}


	public string Title {
		get {
			return _title;
		}
		set {
			_title = value;
		}
	}

	public string PathOnline {
		get {
			return _pathOnline;
		}
		set {
			_pathOnline = value;
		}
	}

	public string PathLocal {
		get {
			return _pathLocal;
		}
		set {
			_pathLocal = value;
		}
	}

	public bool Loaded {
		get {
			return _loaded;
		}
		set {
			_loaded = value;
		}
	}

	public bool Online {
		get {
			return _online;
		}
		set {
			_online = value;
		}
	}

	public XmlDocument XmlRootDoc{
		get {
			return _xmlRootDoc;
		}
		set {
			_xmlRootDoc = value;
		}
	}

	public XmlNode PathOnlineNode
	{
		get {
			return _pathOnlineNode;
		}
		set {
			_pathOnlineNode = value;
		}
	}

	public XmlNode PathLocalNode
	{
		get {
			return _pathLocalNode;
		}
		set {
			_pathLocalNode = value;
		}
	}

	public void SaveLocalXml(){
		XmlRootDoc.Save(MainController.MAINFILEPATH);
	}

	public void SelectFileDescriptor(){
		_mc.SelectFileDescriptor(this);
	}

}
