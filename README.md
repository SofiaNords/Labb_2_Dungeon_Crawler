# Lab 2 - Dungeon Crawler

## The Task

### Lab 2 - Dungeon Crawler

En dungeon crawler �r en typ av rollspel som involverar att spelare utforskar labyrintiska omr�den, s� kallade dungeons, d�r de sl�ss mot fiender och letar efter skatter. I denna labb bygger vi en, n�got f�renklad, version av ett s�dant spel i form av en konsolapplikation.

Spelgenren roguelike bygger oftast p� s� kallad procedural generation, som �r en metod f�r ta fram slumpm�ssiga banor med hj�lp av algoritmer. Eftersom fokus p� denna labb ska vara objektorienterad programmering, s� har jag valt bort den delen och ist�llet skapat en f�rdig bana som ni f�r i form av en textfil. (Ladda ner Level1.txt, l�ngst ner p� sidan.)

Filen representerar en �dungeon� med tv� olika sorters monster (�rats� & �snakes�) utplacerade, och har �ven en f�rdefinierad startposition f�r spelaren. Din uppgift blir att skriva kod som l�ser in filen och delar in i olika objekt (v�ggar, spelare och fiender) i C# som frist�ende fr�n varandra h�ller reda p� sina egna data (t.ex. position, f�rg, h�lsa) och metoder (t.ex f�r att f�rflytta sig, eller attackera).

## Klasshierarki av Level Elements

F�rutom sj�lva spelaren finns det 3 olika sorters objekt i v�r �dungeon�: �Wall�, �Rat�, och �Snake�. Vi vill anv�nda arv f�r att �teranv�nda s� mycket kod som m�jligt f�r den funktionalitet som delas av flera typer av objekt.

Det ska finns en abstrakt basklass som jag valt att kalla �LevelElement�. Eftersom den �r abstrakt s� kan man inte ha instanser av denna, utan den anv�nds f�r att definiera basfunktionalitet som andra klasser sedan kan �rva. LevelElement ska ha properties f�r (X,Y)-position, en char som lagrar vilket tecken en klass ritas ut med (t.ex. kommer �Wall� anv�nda #-tecknet), samt en ConsoleColor som lagrar vilken f�rg tecknet ska ritas med. Den ska dessutom ha en publik Draw-metod (utan parametrar), som vi kan anropa f�r att rita ut ett LevelElement med r�tt f�rg och tecken p� r�tt plats.

Klassen �Wall� �rver av LevelElement, och beh�ver egentligen ingen egen kod f�rutom att h�rdkoda f�rgen och tecknet f�r v�gg (en gr� hashtag).

Klassen �Enemy� �rver ocks� av LevelElement, och l�gger till funktionalitet som �r specifik f�r fiender. �ven Enemy �r abstrakt, d� vi inte vill att man ska kunna instansiera ospecifika �fiender�. Alla riktiga fiender (i labben rat & snake, men om man vill och har tid f�r man l�gga till fler typer av fiender) �rver av denna klass. Enemy ska ha properties f�r namn (t.ex snake/rat), h�lsa (HP), samt AttackDice och DefenceDice av typen Dice (mer om detta l�ngre ner). Den ska �ven ha en abstrakt Update-metod, som allts� inte implementeras i denna klass, men som kr�ver att alla som �rver av klassen implementerar den. Vi vill allts� kunna anropa Update-metoden p� alla fiender och sedan sk�ter de olika subklasserna hur de uppdateras (till exempel olika f�rflyttningsm�nster).

Slutligen har vi klasserna �Rat� och �Snake� som initialiserar sina ned�rvda properties med de unika egenskaper som respektive fiende har, samt �ven implementerar Update-metoden p� sina egna unika s�tt.

## L�sa in bandesign fr�n fil

Skapa en klass �LevelData� som har en private field �elements� av typ List<LevelElement>. Denna ska �ven exponeras i form av en public readonly property �Elements�.

Vidare har LevelData en metod, Load(string filename), som l�ser in data fr�n filen man anger vid anrop. Load l�ser igenom textfilen tecken f�r tecken, och f�r varje tecken den hittar som �r n�gon av #, r, eller s, s� skapar den en ny instans av den klass som motsvarar tecknet och l�gger till en referens till instansen p� �elements�-listan. T�nk p� att n�r instansen skapas s� m�ste den �ven f� en (X/Y) position; d.v.s Load beh�ver allts� h�lla reda p� vilken rad och kolumn i filen som tecknet hittades p�. Den beh�ver �ven spara undan startpositionen f�r spelaren n�r den st�ter p� @.

N�r filen �r inl�st b�r det allts� finnas ett objekt i �elements� f�r varje tecken i filen (exkluderat space/radbyte), och en enkel foreach-loop som anropar .Draw() f�r varje element i listan b�r nu rita upp hela banan p� sk�rmen.

## Game Loop

En game loop �r en loop som k�rs om och om igen medan spelet �r ig�ng, och i v�rat fall kommer ett varv i loopen motsvaras av en omg�ng i spelet. F�r varje varv i loopen inv�ntar vi att anv�ndaren trycker in en knapp; sedan utf�r vi spelarens drag, f�ljt av datorns drag (uppdatera alla fiender), innan vi loopar igen. M�jligtvis kan man ha en knapp (Esc) f�r att avsluta loopen/spelet.

N�r spelaren/fiender flyttar p� sig beh�ver vi ber�kna deras nya position och leta igenom alla v�r LevelElements f�r att se om det finns n�got annat objekt p� den platsen man f�rs�ker flytta till. Om det finns en v�gg eller annat objekt (fiende/spelaren) p� platsen m�ste f�rflyttningen avbrytas och den tidigare positionen g�lla. Notera dock att om spelaren flyttar sig till en plats d�r det st�r en fiende s� attackerar han denna (mer om detta l�ngre ner). Detsamma g�ller om en fiende flyttar sig till platsen d�r spelaren st�r. Fiender kan dock inte attackera varandra i spelet.

## Vision range

F�r att f� en effekt av �utforskande� i spelet begr�nsar vi spelarens synf�lt till att bara visa objekt inom en radie av 5 tecken (men ni kan ocks� prova med andra radier); V�ggarna f�rsvinner dock aldrig n�r man v�l sett dem, men fienderna syns inte s� fort de kommer utanf�r radien.

Avst�ndet mellan tv� punkter i 2D kan enkelt ber�knas med hj�lp av pythagoras sats.

## Kasta t�rningar

Spelet anv�nder sig av simulerade t�rningskast f�r att avg�ra hur mycket skada spelaren och fienderna g�r p� varandra.

Skapa en klass �Dice� med en konstruktor som tar 3 parametrar: numberOfDice, sidesPerDice och Modifier. Genom att skapa nya instans av denna kommer man kunna skapa olika upps�ttningar av t�rningar t.ex �3d6+2�, d.v.s slag med 3 stycken 6-sidiga t�rningar, d�r man tar resultatet och sedan plussar p� 2, f�r att f� en total po�ng.

Dice-objekt ska ha en publik Throw() metod som returnerar ett heltal med den po�ng man f�r n�r man sl�r med t�rningarna enligt objektets konfiguration. Varje anrop motsvarar allts� ett nytt kast med t�rningarna.

G�r �ven en override av Dice.ToString(), s� att man n�r man skriver ut ett Dice-objekt f�r en str�ng som beskriver objektets konfiguration. t.ex: �3d6+2�.

## Attack och f�rsvar

N�r en spelare attackerar (g�r in i) en fiende, eller tv�rtom s� beh�ver vi simulera t�rningskast f�r att f� en po�ng som avg�r hur mycket skada attacken g�r. Den som attackerar kastar d� sina t�rningar f�rst och f�r en attackpo�ng. Sedan kastar den som f�rsvarar sina t�rningar och f�r en f�rsvarspo�ng. Ta sedan attackpo�ngen minus f�rsvarspo�ngen, och om differensen �r st�rre �n 0, dra motsvarande antal fr�n f�rvararens HP (h�lsopo�ng). Efter en eller flera attacker kommer HP ner till 0, varp� fienden d�r (eller spelaren f�r game over).

Om f�rsvararen �verlever kommer han direkt att g�ra en motattack, d.v.s kasta t�rningar p� nytt f�r att f� en attackpo�ng; varp� den som f�rst attackerade nu f�rsvarar genom att kasta sina t�rningar. Dra bort HP enligt reglerna ovan.

 Spelaren samt alla typer av fiender har en upps�ttning t�rningskonfigurationer f�r sin attack respektive f�rsvar, samt en h�rdkodad HP som man startar med. Jag har anv�nt f�ljande konfigurationer, men ni f�r g�rna prova med andra:

Player: HP = 100, Attack = 2d6+2, Defence = 2d6+0

Rat: HP = 10, Attack = 1d6+3, Defence = 1d6+1

Snake: HP = 25, Attack = 3d4+2, Defence = 1d8+5 

## F�rflyttningsm�nster

Spelaren f�rflyttar sig 1 steg upp, ner, h�ger eller v�nster varje omg�ng, alternativt st�r still, beroende p� vilken knapp anv�ndaren tryckt p�.

Rat f�rflyttar sig 1 steg i slumpm�ssig vald riktning (upp, ner, h�ger eller v�nster) varje omg�ng.

Snake st�r still om spelaren �r mer �n 2 rutor bort, annars f�rflyttar den sig bort fr�n spelaren.

Varken spelare, rats eller snakes kan g� igenom v�ggar eller varandra.

