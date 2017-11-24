namespace Arena.Presentation {

  public struct Fighter {
    public string Id;

    public Fighter(string id) {
      Id = id;
    }

    public static Fighter InitialState() {
      return new Fighter("");
    }
  }

}
