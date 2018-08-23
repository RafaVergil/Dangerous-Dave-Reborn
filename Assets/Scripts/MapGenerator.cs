//Esse aqui é um código normal, como você já tá acostumado. Mas ao invés de nos ajudar no jogo, ele nos ajuda no editor.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System.Linq;

public class MapGenerator : MonoBehaviour {

	/*Então vamos lá: como eu queria criar um mapa baseado em tiles (tipo RPG Maker),
	 * o mapa seria então uma tabela de sprites. Eu queria um mapa que fosse 19x10,
	 * então seriam 190 sprites pra cuidar, posicionar e etc. Pra evitar todo esse trabalho,
	 * a ideia desse código é criar um único sprite e salvar ele como prefab.
	 * Aí esse código vai pedir as dimensões no mapa e aí vai instanciar esse prefab,
	 * já posicionando tudo certinho no seu devido lugar.
	 * 
	*/

	public MapItem mapItemPrefab; //Prefab da sprite. Depois visite esse código pra ver o que ele faz.
	public float pixelSize = .46f; //Importante saber o tamanho do pixel, pra posicionamento
	public Vector2 gridSize = new Vector2(20, 10); //tamanho do mapa.  Aqui diz 20x10, mas dá pra trocar pelo editor. Então 20x10 dá 200 sprites.
	public List<MapItem> mapItems; //Uma lista pra ter controle sobre todos os tiles do mapa.

	public void GenerateGrid(){ //Esse método é o que cria o mapa

		foreach (MapItem g in mapItems) { //Se você gerar o mapa 2 vezes, essa parte do código vai destruir os sprites antes de faze-los de novo.
			//Caso contrário você geraria 200 sprites, depois mais 200, e mais 200, e aí caga tudo.
			Destroy (g.gameObject);
		}

		mapItems.Clear ();

		//Aqui o mapa tá sendo criado. Varre x e y
		for (int i = 0; i < gridSize.x; i++) {
			for (int j = 0; j < gridSize.y; j++) {
				//Instancia o prefab, posiciona ele.
				//o sprite 1x1 fica na posicao 0, 0. o 1x2 fica na posicao 0, 0.46
				//o sprite 2x2 fica na posicao 0.46, 0.46 e assim vai.
				MapItem mapItem = GameObject.Instantiate (mapItemPrefab, new Vector2 (i * pixelSize, j * pixelSize), Quaternion.identity);
				mapItem.transform.parent = transform; //coloco todos os sprites gerados dentro dum objeto, pra deixar organizado.
				mapItems.Add (mapItem); //adiciono à lista de referencias
				mapItem.ConfigureSelf (); // chamo o método que vai trocar a sprite (colocar a sprite de porta no item que é do tipo porta
				//colocar sprite de parede no item que é do tipo parede, etc.
				//Depois veja o código MapItem pra ver como isso é feito. Ele também tem um código de editor, o _editorMapItem
			}
		}
	}

	public void RemoveRemovables(){ 
		//no MapItem eu criei o tipo "removable", são os sprites que quero deletar.
		//Esse método deleta eles.
		foreach (MapItem g in mapItems) {
			if (g.type == MapItem.MapItemType.REMOVABLE) {
				DestroyImmediate (g.gameObject);
			}
		}

		//Isso aqui é LINQ. Não manjo muito, peguei na net esse trecho que 
		//da lista de referências todos os itens que estão nulos (no caso, os que foram destruídos no código acima)
		mapItems = mapItems.Where(item => item != null).ToList();
			
	}

	public void Clear(){

		//Mesmo do método acima, mas deleta não só os itens do tipo Removable, mas deleta tudo.
		foreach (MapItem g in mapItems) {
			DestroyImmediate (g.gameObject);
		}

		mapItems = mapItems.Where(item => item != null).ToList();

	}

	public void ConfigureAll(){
		//Aqui cada tile tem sua sprite setada de acordo com seu tipo.
		foreach (MapItem g in mapItems) {
			g.ConfigureSelf ();
		}
	}

}