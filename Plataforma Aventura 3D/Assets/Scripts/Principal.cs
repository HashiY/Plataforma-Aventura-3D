using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Principal : MonoBehaviour {

	int _ovos;
	int _penas = 4;
	int _vidas;

	int contaPisca;

	GameObject jogador;

	public Image imagemVidas;
	public Image iconeFelpudo;
	public Text textoVidas;
	public Text textoOvos;

	public Sprite[] iconesHudVida;
    
	public GameObject objetoVidro;

	public GameObject cameraIntro;
	public GameObject cameraJogador;
	//public GameObject cameraEstrela;


	void Start(){

		jogador = GameObject.Find("Jogador");
        // para usar em qualquer resoluçao de tela
        //largura dele /2 +10 no eixo X  ~~  altura da tela - ~largura dele/2 -10
		iconeFelpudo.transform.position = new Vector2(iconeFelpudo.GetComponent<RectTransform>().sizeDelta.x/2 + 10 , Screen.height - iconeFelpudo.GetComponent<RectTransform>().sizeDelta.y/2 - 10);
		imagemVidas.transform.position = new Vector2(Screen.width/2 ,Screen.height - imagemVidas.GetComponent<RectTransform>().sizeDelta.y/2 - 10); 
		textoOvos.transform.position = new Vector2(Screen.width - textoOvos.GetComponent<RectTransform>().sizeDelta.x/2 -10 , Screen.height - textoOvos.GetComponent<RectTransform>().sizeDelta.y/2 - 10);

		var objects = GameObject.FindGameObjectsWithTag("Ovo"); // numeros de ovos q existem na cena 
		_ovos = objects.Length; //tamanho desse var 
		textoOvos.text = _ovos.ToString();

		imagemVidas.GetComponent<Image>().sprite = iconesHudVida[_penas]; // vai pegar os sprites da viada e começa no 4 pois pena=4

		//cameraEstrela.SetActive(false);
		cameraIntro.SetActive(true);
		cameraJogador.SetActive(true);
	}
	public void PegaOvo(){
		_ovos--;
		if(_ovos<=0) // se for menor q 0
		{
			_ovos=0; // 0
			PegouTodosOvos();
		}
		textoOvos.text = _ovos.ToString();
	}
	public void PegaPena(){
		_penas++;
		if (_penas>8){_penas=8;} // se tiver mais q 8 enas = 8 
		imagemVidas.GetComponent<Image>().sprite = iconesHudVida[_penas];
	}

	public void PerdePena(){ // se recebe o dano
		_penas--;
		if (_penas<=0){_penas=0; PerdeJogo();}
		imagemVidas.GetComponent<Image>().sprite = iconesHudVida[_penas];
	}

	public void PegaEstrela(){Invoke("RecarregaCena", 2f);} // se pega a estrela

	void PegouTodosOvos(){
		//cameraEstrela.SetActive(true);
		cameraIntro.SetActive(false);
		cameraJogador.SetActive(false);
		Invoke("SomeVidro", 1.5f); // para sumir com o vidro
	}
	void GanhaJogo(){}
	void PerdeJogo(){ Invoke("RecarregaCena", 2f); }

	void SomeVidro(){
		objetoVidro.SetActive(false);
		Invoke("VoltaCamera", 1.5f);

	}

	void VoltaCamera(){
		//cameraEstrela.SetActive(false);
		cameraIntro.SetActive(false);
		cameraJogador.SetActive(true);
	}

	public void CaiuNoBuraco(){ // se cai
		Invoke("RecarregaCena", 2f);

	}
	public void RecarregaCena(){//recarrega a cena
		Application.LoadLevel("Felpudo3DAdventure");

	}

	void EfeitoDePancada(){ //quando se colide com o fogo
		PerdePena();
		InvokeRepeating("PiscaFelpudo",0,0.15f);
		jogador.GetComponent<CharacterController>().Move(jogador.transform.TransformDirection(Vector3.back));
        // e empurrado para tras
	}

	void PiscaFelpudo() // piscar
	{
		contaPisca++;
		jogador.SetActive(!jogador.activeInHierarchy); //desativa e ativa em hierarquia
		 
		if (contaPisca>7) {contaPisca=0;jogador.SetActive(true); CancelInvoke("PiscaFelpudo");}
        // cancela
	}
}
