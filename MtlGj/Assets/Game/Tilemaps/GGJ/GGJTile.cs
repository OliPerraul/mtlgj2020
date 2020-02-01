using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.Tilemaps.T

public enum TileID
{
    Start,
    End,
    Full,
}


public class GGJTile : RuleTile
{
    [SerializeField]
    public TileID ID;

    //[SerializeField]
    //private Color _color;

    //public void OnValidate()
    //{
    //    Color = _color;
    //}

}
