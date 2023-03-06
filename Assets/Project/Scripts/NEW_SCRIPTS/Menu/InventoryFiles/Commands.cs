using UnityEngine;


//this should be reworked so command functions can be added and will automatically show up without
//being put in the switch statement
//also needs error handling in case function cannot be found
public class Commands : MonoBehaviour
{
    private string CommandText;
    public InventoryManager inventory;
    private void Start()
    {

        inventory = gameObject.GetComponent<InventoryManager>();
    }
    public dynamic ParseCommand(string String)
    {
        for (int i = 0; i < GameData.Data.ItemList.Count; i++)
        {
            if (GameData.Data.ItemList[i].ID == String)
            {
                Items item = GameData.Data.ItemList[i];
                return item;
            }
        }
        for (int i = 0; i < GameData.Data.EntityList.Count; i++)
        {
            if (GameData.Data.EntityList[i].ID == String)
            {
                Entity entity = GameData.Data.EntityList[i];
                return entity;
            }
        }
        return null;
    }
    public void InputCommand(string input)
    {
        CommandText = input;
    }
    public void EnterCommand()
    {
        string[] attributes = CommandText.Split(' ');
        switch (attributes[0])
        {
            case "give":
                GiveItem(attributes[1], int.Parse(attributes[2]));
                break;
            case "remove":
                TakeItem(attributes[1], int.Parse(attributes[2]));
                break;
            case "spawn":
                SpawnEntity(attributes[1]);
                break;
            case "rosebud":
                AllItems();
                break;
            case "clear":
                ClearInventory();
                break;
            case "chunkgen":
                ChunkGen(int.Parse(attributes[1]), int.Parse(attributes[2]));
                break;
            case "loadlevel":
                LoadLevel(int.Parse(attributes[1]));
                break;
        }
    }
    public void GiveItem(string item, int amount)
    {
        inventory.PickupItem(ParseCommand(item), amount);
        Debug.Log("recieved " + amount + " " + item);
    }
    public bool TakeItem(string item, int amount)
    {
        return inventory.RemoveItem(ParseCommand(item), amount);
    }
    public void AllItems()
    {
        for (int i = 0; i < GameData.Data.ItemList.Count; i++)
        {
            inventory.PickupItem(GameData.Data.ItemList[i], GameData.Data.ItemList[i].StackSize);
        }
    }
    public void ClearInventory()
    {
        for (int i = 0; i < GameData.Data.ItemList.Count; i++)
        {
            inventory.RemoveItem(GameData.Data.ItemList[i], GameData.Data.ItemList[i].StackSize);
        }
    }
    public void SpawnEntity(string enemy)
    {
        Vector2 spawnpoint = Random.onUnitSphere * 2;
        GameObject spawnedenemy = Instantiate(ParseCommand(enemy), GameData.Data.player.transform.position + (Vector3)spawnpoint, Quaternion.identity, GameData.Data.EntitySpawn.transform);
    }
    private void ChunkGen(int cx, int cy)
    {
        //obselete testing code, should remove
        GameData.Data.gameObject.GetComponent<GameMenu>().TileMap.GetComponent<TileMap>().CreateChunk(cx, cy);
    }
    private void LoadLevel(int level)
    {
        Debug.Log("changed level from " + GameData.Data.gameObject.GetComponent<GameMenu>().TileMap.GetComponent<World>().CurrentLevel + " to " + level);
        GameData.Data.gameObject.GetComponent<GameMenu>().TileMap.GetComponent<World>().SetLevel(level);
        //GameData.Data.gameObject.GetComponent<GameMenu>().TileMap.GetComponent<World>().CurrentLevel = level;
        //GameData.Data.gameObject.GetComponent<GameMenu>().TileMap.GetComponent<Map>().currentLevel = level;
        //GameData.Data.gaemeObject.GetComponent<GameMenu>().TileMap.GetComponent<World>().UnloadLevel();
    }
}
