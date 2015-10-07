using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShowController : MonoBehaviour {

	public RectTransform indexGroup;
	public GameObject indexPrefab;
	public GameObject buttonNext;
	public GameObject buttonPrevious;

	private Button _buttonNext;
	private Button _buttonPrevious;

	public List<Section> _sections = new List<Section>();

	private int _activeSectionIndex = 0;

	// Use this for initialization
	void Start () {
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		try {
			
			xmlDoc.Load (MainController.SelectedFiledescriptor.PathLocal);
			
			XmlNodeList nodesList = xmlDoc.GetElementsByTagName("section"); // array of the level nodes.
			
			if (nodesList.Count == 0){
				Debug.Log("no section nodes");
			}
			else {
				foreach(XmlNode xn in nodesList){
					GameObject go = (GameObject)Instantiate(indexPrefab); 
					go.transform.SetParent(indexGroup.transform, false);
					Section section = new Section(go);
					_sections.Add(section);

					/*
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
					fe.fileDescriptor = fd;*/
				}
				if (_sections.Count >0) _sections[0].SetActive();
				if (buttonPrevious != null){
					_buttonPrevious = (Button)buttonPrevious.GetComponent<Button>();
					_buttonPrevious.interactable = false;
				}
				if (buttonNext != null){
					_buttonNext = (Button)buttonNext.GetComponent<Button>();
					if (_sections.Count <= 1){
						_buttonNext.interactable = false;
					}
				}



				/*
				for (int i = 0; i < _sections.Count; i++){
					if (i == 0){
						_sections[i].
					}
				}*/
			}
			
		}
		catch(XmlException ex){
		
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NavigateNext(){
		_sections[_activeSectionIndex].SetInactive();
		_activeSectionIndex++;
		_sections[_activeSectionIndex].SetActive();
		if (_activeSectionIndex == _sections.Count-1){
			_buttonNext.interactable = false;
		}
		_buttonPrevious.interactable = true;
	}

	public void NavigatePrevious(){
		_sections[_activeSectionIndex].SetInactive();
		_activeSectionIndex--;
		_sections[_activeSectionIndex].SetActive();
		if (_activeSectionIndex == 0){
			_buttonPrevious.interactable = false;
		}
		_buttonNext.interactable = true;
	}
}
