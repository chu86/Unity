using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class FileElement : MonoBehaviour {

	public FileDescriptor fileDescriptor;

	public GameObject titleText;
	public GameObject inputPathText;
	public GameObject localPathText;
	public GameObject statusText;

	// Use this for initialization
	void Start () {
		if (fileDescriptor != null) {
			titleText.GetComponent<Text> ().text = fileDescriptor.Title; 
			inputPathText.GetComponent<InputField> ().text = fileDescriptor.PathOnline; 
			localPathText.GetComponent<Text> ().text = fileDescriptor.PathLocal; 
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsOnlineFile{
		get {
			if (fileDescriptor != null){
				return fileDescriptor.Online;
			}
			else {
				return false;
			}
		}
	}

	public void DownloadFile(){
		StartCoroutine (DownloadFileIterator ());
	}

	public IEnumerator DownloadFileIterator(){
		if (IsOnlineFile) {
			WWW www = new WWW (fileDescriptor.PathOnline);
			//yield return www;

			while (!www.isDone) {
				statusText.GetComponent<Text> ().text = "downloaded " + (www.progress * 100).ToString () + "%...";
				yield return null;
			}
			string fullPath = fileDescriptor.PathLocal;
			if (fileDescriptor.PathLocal == ""){
				//string fileName = fileDescriptor.Title + Guid.NewGuid().ToString("N");
				string fileName = fileDescriptor.Title;
				foreach (char c in System.IO.Path.GetInvalidFileNameChars())
				{
					fileName = fileName.Replace(c, '_');
				}
				fullPath = Application.persistentDataPath + "/"+fileName+".xml";
			}

			File.WriteAllBytes (fullPath, www.bytes);

			fileDescriptor.PathLocal = fullPath;
			fileDescriptor.PathLocalNode.InnerText = fullPath;
			fileDescriptor.SaveLocalXml();
			localPathText.GetComponent<Text> ().text = fileDescriptor.PathLocal; 

			statusText.GetComponent<Text> ().text = "downloaded";
		} else {
			Debug.Log("not an online file");
		}
	}

	public void Select(){
		fileDescriptor.SelectFileDescriptor();
	}
}
