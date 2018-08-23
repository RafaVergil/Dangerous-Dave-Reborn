/* Esse código fica embutido no prefab das sprites.
A ideia é que ele se configure automaticamente, pra gente nao
precisar ficar escolhendo qual sprite vai pra qual prefab. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MapItem : MonoBehaviour {

	//Enum pra falar quais os possiveis tipos de coisas que vao ter nos mapas.
	public enum MapItemType {
		TUBE = 0,
		DOOR = 1,
		TROPHY = 2,
		ITEM_1 = 3,
		ITEM_2 = 4,
		ITEM_3 = 5,
		WALL_RED = 6,
		REMOVABLE = 7,
		SPAWN = 8,
		WALL_BLUE = 9,
	}

	public MapItemType type = MapItemType.WALL_RED; //todos os MapItems começam como parede vermelha

	public SpriteRenderer currentSprite; //referencia pro componente de Sprite

	//referencia para todas as possiveis sprites do jogo.
	public Sprite spriteTube;
	public Sprite spriteDoor;
	public Sprite spriteTrophy;
	public Sprite spriteItem1;
	public Sprite spriteItem2;
	public Sprite spriteItem3;
	public Sprite spriteWallRed;
	public Sprite spriteRemovable;
	public Sprite spriteSpawn;
	public Sprite spriteWallBlue;

	//Quando o mapa é gerado ou quando a gente clica no botão Configure Self no editor, este método é chamado
	public void ConfigureSelf(){
		//coloca esse objeto na layer default
		gameObject.layer = LayerMask.NameToLayer("Default");

		switch (type) { //verifica o tipo
		case MapItemType.TUBE: //se for do tipo tubo
			currentSprite.sprite = spriteTube; //coloca a sprite de tubo
			gameObject.layer = LayerMask.NameToLayer("groundMask"); //coloca na layer de chão, pq podemos andar sobre o tubo
			break;
		case MapItemType.DOOR:{ //se for do tipo porta
			currentSprite.sprite = spriteDoor; //coloca a sprite de porta
				gameObject.layer = LayerMask.NameToLayer("door"); //coloca na layer de porta
				Collider2D col = GetComponent<Collider2D> ();
				if (col) {
					col.isTrigger = true; //diz que é trigger porque, diferente das paredes, o jogador pode passar através da porta
				}
			}
			break;

			//Mesmo esquema da porta pros itens coletáveis e pro Trofeu
		case MapItemType.TROPHY:{
			currentSprite.sprite = spriteTrophy;
				gameObject.layer = LayerMask.NameToLayer("collectable");
			Collider2D col = GetComponent<Collider2D> ();
			if (col) {
				col.isTrigger = true;
			}
			}
			break;
		case MapItemType.ITEM_1:{
			currentSprite.sprite = spriteItem1;
				gameObject.layer = LayerMask.NameToLayer("collectable");
			Collider2D col = GetComponent<Collider2D> ();
			if (col) {
				col.isTrigger = true;
			}
			}
			break;
		case MapItemType.ITEM_2:{
			currentSprite.sprite = spriteItem2;
				gameObject.layer = LayerMask.NameToLayer("collectable");
			Collider2D col = GetComponent<Collider2D> ();
			if (col) {
				col.isTrigger = true;
			}
			}
			break;
		case MapItemType.ITEM_3:{
			currentSprite.sprite = spriteItem3;
				gameObject.layer = LayerMask.NameToLayer("collectable");
			Collider2D col = GetComponent<Collider2D> ();
			if (col) {
				col.isTrigger = true;
			}
			}
			break;
			default:
		case MapItemType.WALL_RED:
			currentSprite.sprite = spriteWallRed;
			gameObject.layer = LayerMask.NameToLayer("groundMask");
			break;
		case MapItemType.REMOVABLE:
			currentSprite.sprite = spriteRemovable;
			break;
		case MapItemType.SPAWN:
			currentSprite.sprite = spriteSpawn;
			break;

		case MapItemType.WALL_BLUE:
			currentSprite.sprite = spriteWallBlue;
			gameObject.layer = LayerMask.NameToLayer("groundMask");
			break;
		}
	}


}
