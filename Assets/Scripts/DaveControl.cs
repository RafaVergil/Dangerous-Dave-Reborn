using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class DaveControl : MonoBehaviour {

	//Aqui é a movimentação do personagem. É igual ao seu código, mas sem dash e sem extra jump.


	//explicando essa parte de _instance:
	//isso é chamado de Singleton. É uma forma de ter acesso a esse objeto durante o jogo inteiro, entre as cenas
	//Sem precisar ficar criando referencias. Como _instance é "static", nao precisamos criar referencia pra classe DaveControl
	//Basta chamar DaveControl.nomedometodoOuVariavel
	//No caso, _instance esta como privado, entao nao da pra acessar
	private static DaveControl _instance;
	public static DaveControl getInstance(){ //mas fiz esse metodo que eh publico e que retorna o _instance. OLHAR METODO AWAKE()
		return _instance;
	}

	public float moveInput = 0f;
	public float speed = 3f;
	public Rigidbody2D rb;
	public bool isGrounded = false;
	public LayerMask groundMask;
	public float jumpForce = 4.5f;
	public Transform groundCheckTL;
	public Transform groundCheckBR;
	public bool hasTrophy = false;
	public float score = 0;
	public int lives = 3;
	public int currentLevel = 1;



	//Aqui no Awake, o que ocorre eh o seguinte
	void Awake () {

		//SE ja ouver um _instance existente e esse _instance nao é esse objeto aqui, então destrói esse objeto.
		if (_instance != null && _instance != this) {
			Destroy (gameObject);
			return;
		} else { //Caso contrario, se o _instance for nulo, fala que agora o _instance eh esse objeto aqui.
			_instance = this;
		}

		//Isso evita duplicatas. Se a gente passar varias vezes por uma cena que crie esse objeto, soh um vai
		//existir. O resto vai ser destruido.
		//Olhar o START()
	}

	void Start(){
		//Esse DontDestroyOnLoad eh o que mantem o objeto entre as cenas. Qualquer objeto sem esse codigo vai ser destruido na troca de cenas.
		DontDestroyOnLoad (gameObject);
		rb = GetComponent<Rigidbody2D> ();

		if (HUDControl.getInstance ()) {
			HUDControl.getInstance ().UpdateHud ();
		}
	}
	

	void FixedUpdate () {

		isGrounded = Physics2D.OverlapArea(groundCheckTL.position, groundCheckBR.position, groundMask);

		moveInput = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
		{
			rb.velocity = Vector2.up * jumpForce;
		}
	
	}

	//Esse metodo lida com os possiveis eventos quando o personagem toca algo no mapa
	void OnTriggerEnter2D(Collider2D col){
		MapItem mapItem = col.gameObject.GetComponent<MapItem> ();
		if (mapItem) {

			//se tocou num trofeu ou item, aumenta a pontuacao.
			//se tocou na porta, passa pra proxima fase, mas apenas se o trofeu tiver sido pego
			//Aqui tambem vai ter a condicao de se tocar na agua, no fogo, num tiro, num inimigo, etc. O personagem vai morrer e perder uma vida.
			switch (mapItem.type) {
			case MapItem.MapItemType.TROPHY:
				hasTrophy = true;
				score += 1000;
				col.gameObject.SetActive (false);
				break;
			case MapItem.MapItemType.DOOR:
				if (hasTrophy) {
					currentLevel++;
					SceneManager.LoadScene (Constants.SCENE_NAME_TRANSITION);
				}
				break;

			case MapItem.MapItemType.ITEM_1:
				score += 50;
				col.gameObject.SetActive (false);
				break;

			case MapItem.MapItemType.ITEM_2:
				score += 100;
				col.gameObject.SetActive (false);
				break;

			case MapItem.MapItemType.ITEM_3:
				score += 150;
				col.gameObject.SetActive (false);
				break;
			}

			if (HUDControl.getInstance ()) {
				HUDControl.getInstance ().UpdateHud ();
			}
		}
	}

}
