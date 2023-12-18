using System;
using System.IO;
using UnityEngine;

public class BobikController : MonoBehaviour, IPlayer
{
    public event Action OnKilled;

    public PlayerData playerData = new();
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _dampingSpeed;
    [SerializeField] private KeyCode _JumpButton;
    [SerializeField] private SpriteRenderer _spriteBob;
   // [SerializeField] private Camera _camera;

    public void MakeDamage()
    {
        _rb.AddForce(Vector2.up * _jumpForce);
        GetComponent<Collider2D>().isTrigger = true;
        OnKilled?.Invoke();
        enabled = false;
    }

    void Update()
    {
        CharacterMovment();
       // -------------------

        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadData();
        }
    }

    private void FixedUpdate()
    {
      //_camera.transform.position = Vector3.Lerp(new Vector3(_camera.transform.position.x, _camera.transform.position.y, -10), transform.position, Time.deltaTime * _dampingSpeed);

        if (Input.GetKeyDown(_JumpButton)) /// прыжки
        {
            _rb.AddForce(Vector2.up * _jumpForce);
        }
    }

    private void CharacterMovment()   /// дефолтный код по движению персонажа
    {
        float inputDir = Input.GetAxis("Horizontal");

        _spriteBob.flipX = inputDir < 0;

        _animator.SetFloat("MoveSpeed", Mathf.Abs(inputDir));

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + inputDir, transform.position.y, 0), Time.deltaTime * _moveSpeed);

        //if (Input.GetKeyDown(_JumpButton)) /// прыжки
        //{
          //  _rb.AddForce(Vector2.up * _jumpForce);
        //}
    }


    public void SaveData() // полное сохранение и загрузка данных движения персонажа
    {
        PlayerData playerData = new PlayerData(transform.position, gameObject.name);

        string json = JsonUtility.ToJson(playerData);

        PlayerPrefs.SetString("JSON", json);

        string path = Path.Combine(Application.streamingAssetsPath, "GAMEDATA.json");  

        File.WriteAllText(path, json);

        Debug.Log("Saved new position:" + json);
    }

    public void LoadData()
    {
        playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("JSON"));

        transform.position = playerData.Position;

        Debug.Log("You returned to saved position:" + playerData.Name);
    }
}
[Serializable]
public class PlayerData //данные персонажа(игрока)
{
    public Vector3 Position;
    public string Name;

    public PlayerData() { }

    public PlayerData(Vector3 position, string name)
    {
        Position = position;
        Name = name;
    }
}
   


