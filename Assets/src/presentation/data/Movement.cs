namespace Arena.Presentation {

  public struct Movement {
    public string Id;

    public Movement(string id) {
      Id = id;
    }

    public static Movement InitialState() {
      return new Movement("");
    }
  }

}
