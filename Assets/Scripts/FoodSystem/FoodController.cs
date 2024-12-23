using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace FoodSystem
{
    public class FoodController : MonoBehaviour
    {
        [SerializeField] private List<Food> foods;

        public void PlaceFoods(Grid grid)
        {
            foreach (var food in foods)
            {
                if (!food.IsActive)
                {
                    var cell = grid.GetAvailableRandomCell();
                    food.ConfigureSelf(cell.X, cell.Y);
                    food.SetLayer(Utilities.FoodLayer);
                    food.InjectGrid(grid);
                    food.Activate();
                }
            }
        }
    }
}
