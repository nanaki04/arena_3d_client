namespace Arena.Presentation {

  public struct Projectile {
    public string Id;

    public Projectile(string id) {
      Id = id;
    }

    public static Projectile InitialState() {
      return new Projectile("");
    }
  }

}
