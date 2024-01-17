using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RestauInteractor : Interactor
{
    RestaurantPlayer restauPlayer;

    protected override void Awake()
    {
        base.Awake();
        restauPlayer = GetComponentInParent<RestaurantPlayer>();
    }

    public CuisineItem GiveCuisine()
    {
        return restauPlayer.GiveCuisine();
    }

    public void ReceiveCuisine(CuisineItem item)
    {
        restauPlayer.ReceiveCuisine(item);
    }
}