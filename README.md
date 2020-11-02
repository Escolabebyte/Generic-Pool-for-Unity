# Generic Pool for Unity

**Note**: This is a simple script for pooling you object and reuse an object if you want, the usage is very simple

# Methods
These are the methods available for use at the moment
-   Instantiate
-   DesactiveAll
-   AddRange
-   Add
-   Clear

## If you want instantiate some object in unity you can call
> **Note:** The **Instantiate**  method are 2 overload to use
> For every interaction with the pool you need to pass the key
> The key is the original prefab to instantiate
> When you call any method you will create a key of the original prefab if it doesn't exist

    Pool.Instantiate<T>

Exemple:

    public GameObject asteroid;

    public void InstantiateAsteroid()
    {
        Pool.Instantiate(asteroid, new Vector3(), Quaternion.identity);
    }

The method Instantiate use the pool by default
If you can instantiate a object but don't use the instantiated object in the pool you can use the overload

    public GameObject asteroid;

    public void InstantiateAsteroid()
    {
        Pool.Instantiate(asteroid, new Vector3(), Quaternion.identity, false);
    }
The object instaiated will be a temporary pool if you can use the object as reusable object
you can call the AddRange

    public GameObject asteroid;
    public void InstantiateAsteroid()
    {
        Pool.Instantiate(asteroid, new Vector3(), Quaternion.identity, false);
        Pool.AddRange(asteroid);
    }
but you can add each one separately

    public GameObject asteroid;
    public void InstantiateAsteroid()
    {
        GameObject temp = Pool.Instantiate(asteroid, new Vector3(), Quaternion.identity, false);
        Pool.Add(asteroid, temp);
    }

The method DesactiveAll will disable all object in the pool

   
    public GameObject asteroid;
    public void Disable()
    {
        Pool.DesactiveAll(asteroid);
    }

If you can clear the pool you can call Clear

    public GameObject asteroid;
    public void Disable()
    {
        Pool.Clear(asteroid);
    }
If you can clear the pool and destroy all object you can call Clear with the true parameter

    public GameObject asteroid;
    public void Disable()
    {
        Pool.Clear(asteroid, true);
    }
