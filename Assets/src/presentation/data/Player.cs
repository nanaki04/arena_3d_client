namespace Arena.Presentation {

  public struct Player {
    public string Id;

    public Player(string id) {
      Id = id;
    }

    public static Player InitialState() {
      return new Player("");
    }
  }

}
