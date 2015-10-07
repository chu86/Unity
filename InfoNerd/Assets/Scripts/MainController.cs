using UnityEngine;
using System.Collections;
using System.Xml;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

	public RectTransform contentGroup;

	public CanvasGroup actionGroup;

	public GameObject fileElementPrefab;

	public static string MAINFILEPATH = Application.persistentDataPath+"/infonerdData.xml";

	public static FileDescriptor SelectedFiledescriptor;
	public static Color IndexActiveColor = Color.red;
	public static Color IndexInactiveColor = Color.black;

	// Use this for initialization
	void Start () {
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		try {
	
			xmlDoc.Load (MAINFILEPATH);

			XmlNodeList nodesList = xmlDoc.GetElementsByTagName("info"); // array of the level nodes.

			if (nodesList.Count == 0){
				Debug.Log("no info nodes");
			}
			else {
				foreach(XmlNode xn in nodesList){
					GameObject filePrefab = (GameObject)Instantiate(fileElementPrefab); 
					filePrefab.transform.SetParent(contentGroup.transform, false);

					FileDescriptor fd = new FileDescriptor(xmlDoc, this);

					foreach(XmlNode child in xn.ChildNodes){
						if (child.Name == "path-online"){
							fd.PathOnlineNode = child;
							fd.PathOnline = child.InnerText;
						}
						if (child.Name == "path-local"){
							fd.PathLocalNode = child;
							fd.PathLocal = child.InnerText;
						}
						else if (child.Name == "title"){
							fd.Title = child.InnerText;
						}
						else if (child.Name == "loaded"){
							if (int.Parse(child.InnerText) == 1){
								fd.Loaded = true;
							}
						}
					}

					FileElement fe = (FileElement)filePrefab.GetComponent<FileElement>();
					fe.fileDescriptor = fd;
				}
			}

		}
		catch(XmlException ex){

			// file not available? create new
			Debug.Log(ex.ToString());

			/*
			using (XmlWriter writer = XmlWriter.Create(Application.persistentDataPath+"/infonerdData.xml"))
			{
				writer.WriteStartDocument();
				writer.WriteStartElement("nodes");
				writer.WriteStartElement("info");
				writer.WriteElementString("title", "New title");
				writer.WriteElementString("path", "irgendwas");
				writer.WriteElementString("loaded", "0");

				writer.WriteEndElement(); // info
				writer.WriteEndElement(); // nodes
				writer.WriteEndDocument();
			}*/
		}



		//Application.persistentDataPath

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SelectFileDescriptor(FileDescriptor fd){

		SelectedFiledescriptor = fd;

		actionGroup.blocksRaycasts = true;
		actionGroup.interactable = true;
	}

	public void Open(){
		Application.LoadLevel(1);

	}


}
