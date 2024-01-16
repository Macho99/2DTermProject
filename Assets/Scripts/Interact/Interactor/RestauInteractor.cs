using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RestauInteractor : Interactor
{
    RestaurantPlayer restauPlayer;

    private void Awake()
    {
        restauPlayer = GetComponentInParent<RestaurantPlayer>();
    }
}