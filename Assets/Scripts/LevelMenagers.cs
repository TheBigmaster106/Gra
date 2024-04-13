using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    //pozycja gracza
    Transform player;

    //prefab przeciwnika
    public GameObject basherPrefab;

    //czas mi�dzy respawnem kolejnego bashera
    public float spawnInterval = 1;

    //czas od ostatniego respawnu
    float timeSinceSpawn;

    //bezpieczna odleg�o�� spawnu
    float spawnDistance = 30;

    //ilosc punkt�w
    int points = 0;

    //licznik punkt�w na ekranie
    public GameObject pointsCounter;

    //licznik czasu na ekranie
    public GameObject timeCounter;

    //ekran ko�ca gry
    public GameObject gameOverScreen;

    //czas do ko�ca poziomu
    public float levelTime = 60f;

    // Start is called before the first frame update
    void Start()
    {
        //zlinkuj aktualna pozycje gracza do zmiennej transform
        player = GameObject.FindWithTag("Player").transform;

        //zerujemy licznik
        timeSinceSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //dodaj do czasu od ostatniego spawnu czas od ostatniej klatki (ostatni update())
        timeSinceSpawn += Time.deltaTime;

        //je�eli d�u�ej ni� jedna sekunda
        if (timeSinceSpawn > spawnInterval)
        {
            //wygeneruj losow� pozycje
            //Vector3 randomPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));

            //wygeneruj randomow� pozycj� na kole o promieniu 1
            Vector2 random = Random.insideUnitCircle.normalized;

            //skonwertuj x,y na x,z i zerow� wysoko��
            Vector3 randomPosition = new Vector3(random.x, 0, random.y);

            //zwielokrotnij odleg�os� od gracza tak, �eby spawn nast�powa� poza kamer�
            randomPosition *= spawnDistance;

            //dodaj do niej pozycje gracza tak, aby nowe wsp�rz�dne by�y pozycj� wzgl�dem gracza
            randomPosition += player.position;

            //sprawdz czy danej miejsce jest wolne
            if (!Physics.CheckSphere(new Vector3(randomPosition.x, 1, randomPosition.z), 0.5f))
            {
                //stworz nowego przeciwnika z istniej�cego prefaba, na pozycji randomPosition z rotacj� domy�ln�
                Instantiate(basherPrefab, randomPosition, Quaternion.identity);

                //wyzeruj licznik
                timeSinceSpawn = 0;
            }
            //je�li miejsce b�dzie zaj�te to program podejmie kolejn� pr�b� w nast�pnej klatce

        }

        //TODO: opracowa� spos�b na przyspieszanie spawnu w niesko�czono�� wraz z d�ugo�cia trwania etapu

        //dodaj do czasu poziomu czas od ostatniej klatki

        if (levelTime < 0)
        {
            GameOver();
        }
        else
        {
            levelTime -= Time.deltaTime;
            UpdateUI();
        }

    }
    public void AddPoints(int amount)
    {
        points += amount;
    }
    //funkcja kt�ra odpowiada za aktualizacje interfejsu
    private void UpdateUI()
    {
        pointsCounter.GetComponent<TextMeshProUGUI>().text = "Punkty: " + points.ToString();
        timeCounter.GetComponent<TextMeshProUGUI>().text = Mathf.Floor(levelTime).ToString()=="-1"?"0": Mathf.Floor(levelTime).ToString();
    }
    //ta funkcja uruchamia si� je�li gracz zginie lub je�li czas si� sko�czy
    public void GameOver()
    {
        //wy��cz sterowanie gracza
        player.GetComponent<PlayerController>().enabled = false;
        player.transform.Find("MainTurret").GetComponent<WeaponController>().enabled = false;

        //wylacz bashery
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject basher in enemyList)
        {
            basher.GetComponent<BasherController>().enabled = false;
        }

        //wyswietl poprawnie wynik na ekranie ko�cowym
        gameOverScreen.transform.Find("FinalScoreText").GetComponent<TextMeshProUGUI>().text = "Wynik koncowy: " + points.ToString();

        //poka� ekran ko�ca gry
        gameOverScreen.SetActive(true);

    }
}