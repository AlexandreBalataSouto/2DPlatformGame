using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator_Script : MonoBehaviour
{
    public static LevelGenerator_Script  staticLevelGenerator; //Singleton

    public List<LevelBlock_Script> allLevelBlocks = new List<LevelBlock_Script>(); //Lista de todos los bloques de nivel DISPONIBLES

    public Transform levelStartPoint;

    public List<LevelBlock_Script> currentLevelBlocks = new List<LevelBlock_Script>(); //Lista de bloques de nivel EN LA ESCENA

    private void Awake()
    {
        staticLevelGenerator = this;
    }

    private void Start()
    {
        GenerateInitialLevelBlocks();
    }

    public void AddLevelBlock() //Funcion para añadir bloques de nivel
    {
        int randomIndex = Random.Range(0, allLevelBlocks.Count); //Indice aleatorio 
                                                    
                                        //Casteo a LevelBlock           Copiamos o instanciamos el gameObject   
        LevelBlock_Script currentBlock = (LevelBlock_Script)Instantiate(allLevelBlocks[randomIndex]);

        currentBlock.transform.SetParent(this.transform, false); //Al bloque de nivel le establecemos como padre el propio LevelGenerator (this)

        Vector3 spawnPosition = Vector3.zero; //Variable Vector3 sin posicion ni nada

        if(currentLevelBlocks.Count == 0) //Si no hay ningun bloque en la escena
        {
            spawnPosition = levelStartPoint.position; //Le asignamos la posicion del LevelStartPosition que esta en la escena
        }
        else
        {
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position; //Si ya hay un bloque, cogemos la posicion del punto de salida del ultimo bloque
        }

        //Esto es una correcion debido a que la posicion con respecto al startPosition y el LevelStartPosition provocan que los bloques vaya hacia abajo
        Vector3 correction = new Vector3(spawnPosition.x - currentBlock.startPoint.position.x,
                                         spawnPosition.y - currentBlock.startPoint.position.y,
                                         0);

        currentBlock.transform.position = correction;
        currentLevelBlocks.Add(currentBlock);

    }//END AddLevelBlock

    public void RemoveOldestLevelBlock() //Funcion para destruir el bloque mas viejo
    {
        LevelBlock_Script oldestBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldestBlock);
        Destroy(oldestBlock.gameObject);

    } //END RemoveOldestLevelBlock

    public void RemoveAllLevelBlocks() //Funcion para borrar todos los bloques
    {
        while(currentLevelBlocks.Count>0)
        {
            RemoveOldestLevelBlock();
        }
    } //END RemoveAllLevelBlocks

    public void GenerateInitialLevelBlocks() //Funcion para generar bloques
    {
        for(int i = 0; i<2; i++)
        {
            AddLevelBlock();
        }
    }//END GenerateInitialLevelBlocks
}
