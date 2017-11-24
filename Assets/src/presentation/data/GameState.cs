using System.Collections.Generic;

namespace Arena.Presentation {

  public struct GameState {
    public List<Player> Players;
    public List<Projectile> Projectiles;
    public List<Fighter> Fighters;
    public List<Movement> Movements;

    public GameState(
      List<Player> players,
      List<Projectile> projectiles,
      List<Fighter> fighters,
      List<Movement> movements
    ) {
      Players = players;
      Projectiles = projectiles;
      Fighters = fighters;
      Movements = movements;
    }

    public static GameState InitialState() {
      return new GameState(
        new List<Player>(),
        new List<Projectile>(),
        new List<Fighter>(),
        new List<Movement>()
      );
    }
  }

}
