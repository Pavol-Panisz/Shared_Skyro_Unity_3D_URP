# skyro assignment

V týchto cvičeniach si doplníme našu 3D skakacku o novú funkcionalitu:
- Jednoduchý enemák, ktorý sa hýbe doľava-doprava a striela po hráčovi, keď je dosť blízko 
- Strieľanie hráča: hráč má buď nekonečný alebo obmedzený počet projektilov, ktoré vie strieľať 

## Strieľanie:

Strieľanie má fungovať pomocou PlayerShooting skriptu, v ktorom je uložený momentálny počet nábojov a maximálny počet nábojov.

Tento skript má mať 2 public void metódy: AddAmmo(int amount) a SetAmmo(int amount) - cieľ je, nech sa dá z Eventov v iných skriptoch tieto metódy volať

Hráč má vystreliť pri stlačení ľavého tlačidla myšky. Pre jednoduchosť nemusí byť strieľanie "automatické": Čiže zakaždým keď chce hráč vystreliť, musí nanovo stlačiť ľavé tlačidlo myšky.
BONUS: Sprav, nech to je predsalen automatické, s nejakým intervalom strieľania.

V skripte má byť cez editor nastaviteľný prefab, ktorý sa má pri strelení spawnúť. V PlayerShooting skripte avšak neudeľujeme tomuto prefabu rýchlosť. Namiesto toho si prefab sám po "narodení sa" udelí rýchlosť v správnom smere.

## Enemák:

Skript na enemákovi nazvi SimpleEnemy.

Enemák sa má dookola istý čas hýbať doľava a potom istý čas doprava. Rýchlosť a aj čas, po ktorom enemák otočí smer, majú byť nastaviteľné v editore.

Enemák sa má hýbať cez fyziku, čiže nech má dynamické Rigidbody a nastavuje sa mu rýchlosť cez rb.linearVelocity

Enemák má v pravidelných intervaloch spawnovať prefaby jeho "bulletov". Podobne ako pri PlayerShootingu majú tieto prefaby mať na sebe skript, ktorý im udelí rýchlosť v správnom smere.

Keď sa enemy bullet dotkne hráča, hráč zomrie (to sa robý tak, že nastavíš Playerov Transform na KillPlayer.PlayerRespawnPoint). Bullet sa vtedy zničí.

Keď sa playerov bullet dotkne enemáka, enemák sa zničí a takisto sa zničí playerov bullet

## Bullety:

Všetky bullety by mali mať na sebe skript s názvom AutoSelfDestruct, ktorý za konfigurovateľný čas ten gameobjekt zničí.

---

# Checks:
- [ ] Enemy
  - [ ] Movement
    - [ ] left, right, forward, backward
    - [ ] rotation
  - [ ] Bullets
    - [ ] Bullet spawning
    - [ ] Bonus: always aiming at player (aimbot)
  - [ ] kill enemy with player bullets
  - [ ] bonus: health
- [ ] Player
  - [x] Movement
  - [x] Shooting
    - [x] Reloading system
    - [x] Semi & Auto firing system
  - [ ] bonus: health

---

# actual notes:
