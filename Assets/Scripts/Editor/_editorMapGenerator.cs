/*Esse é um Script que alterar o Inspector, pra poder colocar os códigos
 * que alteram no editor sem estar no Play*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //Precisa importar isso aqui

[CustomEditor(typeof(MapGenerator))] //Essa linha diz pra Unity que o editor está sendo customizado
public class _editorMapGenerator : Editor { //Aqui a gente herda de Editor, ao invés de MonoBehaviour

	public override void OnInspectorGUI() //Esse é o método que desenha a interface do editor
	{
		DrawDefaultInspector(); //Desenha o editor padrão

		//MapGenerator é a classe que eu criei pra chamar o método de gerar mapa
		MapGenerator mapGenerator = (MapGenerator)target; //Esse target já vem com o script quando a gente herda da classe Editor (linha 10)
		//Ele é na verdade o nosso script que vai fazer as coisas acontecerem no editor.

		/* Pra fazer um código que altere o editor, você precisa de 2 scripts.
		 * Um deles é esse aqui.
		 * Normalmente dou o nome de "_editor" + nome do script que tem os métodos pra fazer as coisas (nesse caso, gerar o mapa no editor)
		 * Então nesse caso temos o _editorMapGenerator pra controlar o editor e o MapGenerator pra mandar o editor fazer as coisas*/

		if(GUILayout.Button("Generate Grid")){ //Desenhando um botão
			mapGenerator.GenerateGrid (); //Que vai chamar lá no script MapGenerator o método GenerateGrid()
		}

		if(GUILayout.Button("Configure All")){
			mapGenerator.ConfigureAll ();
		}

		if(GUILayout.Button("Remove Removables")){
			mapGenerator.RemoveRemovables ();
		}

		if(GUILayout.Button("Clear")){
			mapGenerator.Clear ();
		}

		//Verifique o script MapGenerator agora pra ver o que acontece quando clicamos nesses botões.
	}
}

