# 1920-a1-be-JiriVanhouwe

Concept
-------
Het overkomt iedereen wel eens: je mijmert over afgelopen zomer, een uitstap of evenement en je kan een gelukzalige glimlach niet onderdrukken. Kon je zo'n moment nog maar eens herbeleven... 

Met wat geluk heb je zelf enkele beelden geschoten maar het is niet ontdenkbaar dat jouw vrienden ook enkele parels op hun smartphone hebben staan. Met 'Memories' kan je in enkele klikken herinneringen delen met vrienden en naar hartenlust foto's toevoegen. Op die manier zal geen beeld je nog ontgaan en heb je op een eenvoudige wijze alle foto's van een uitstap verzameld.

Voorbereiding feedback moment en vragen
---------------------------------------
1) Ik moet toegeven dat ik lang heb staan sukkelen met het domein en de databank. Een user heeft meerdere memories en aangezien hij die wil kunnen delen met andere users, behoren memories alsook tot meerdere users. Via een tussentabel hoop ik dit euvel het hoofd te bieden. Lijkt dit u een goede oplossing? (Zie class diagram klasse UserMemory)

2) Om op een snelle manier herinneringen te sharen, lijkt het me tof als elk account vrienden kan toevoegen. De klasse user kent dus een lijst met 'vrienden'. Ik wist niet goed hoe dit te implementeren, ook hier heb ik gekozen voor een tussentabel maar het was niet van de poes om die vorm te geven. Ik denk dat het gelukt is maar iets in mij zegt dat er een betere oplossing moet zijn. Wat denkt u? (Zie class diagram en tabel UserRelation)

3) Moet ik in de MemoriesController ergens een specifiek UserId meegeven zodat de herinneringen van de ingelogde persoon kan laden? Of komt dit in orde zodra een user inlogt?

4) Via MemoriesController kan je een memory aanpassen. Gezien een memory zijn eigen foto's kent, volstaat het dan om (op regel 80 in MemoriesController) de memory aan te passen en op te slaan? Of moet ik eerst de methode AddMemory uit Memory oproepen en nadien opslaan? Of beiden?

5) Ik heb momenteel nog een PhotoRepository staan. Echter kan ik aan alle foto's via Memory. Is het dan nog interessant om dit op te splitsen?


Domeinlaag
----------

CHECK: Het domein bevat minstens 2 geassocieerde klassen

CHECK: Klassen bevatten toestand en gedrag

CHECK: Klassendiagram is aangemaakt, toont de properties, methodes en de associaties

Datalaag
--------

CHECK: DataContext is aangemaakt

CHECK: Mapping is geïmplementeerd 

CHECK: Databank wordt geseed met data -> Ik wil/moet er nog wat extra data in stoppen.

Controller
----------

CHECK: Minstens 1 controller met endpoints voor de CRUD operaties

CHECK: De endpoints zijn gedefinieerd volgens de best practices

CHECK: Enkel de benodigde data wordt uitgewisseld (DTO’s indien nodig)

Swagger
-------

CHECK: De documentatie is opgesteld


Printscreen van het klassendiagram van de domeinlaag 
----------------------------------------------------
![Klassendiagram](https://i.imgur.com/2dGlVH9.png)


 Printscreen van de API zoals weergegeven in swagger
----------------------------------------------------

![SwaggerOverzicht](https://i.imgur.com/jBFyRYF.png)

api/memories - GET
------------------
![SwaggerOverzicht](https://i.imgur.com/cSWA69k.png)

![SwaggerOverzicht](https://i.imgur.com/XscJoko.png)

api/memories - POST
-------------------
![SwaggerOverzicht](https://i.imgur.com/4rIk9N5.png)

![SwaggerOverzicht](https://i.imgur.com/hXhXUQ7.png)

![SwaggerOverzicht](https://i.imgur.com/Qsyct2n.png)

api/memories/id - GET
---------------------
![SwaggerOverzicht](https://i.imgur.com/tc7l1MY.png)

![SwaggerOverzicht](https://i.imgur.com/2gQXoea.png)

![SwaggerOverzicht](https://i.imgur.com/ZmM4fNy.png)

api/memories/id - PUT
---------------------
![SwaggerOverzicht](https://i.imgur.com/8m6KYYi.png)

![SwaggerOverzicht](https://i.imgur.com/fz1eycA.png)

![SwaggerOverzicht](https://i.imgur.com/E4vTEka.png)

api/memories/id - DELETE
------------------------
![SwaggerOverzicht](https://i.imgur.com/oXvnSEQ.png)

![SwaggerOverzicht](https://i.imgur.com/B3meDzL.png)

![SwaggerOverzicht](https://i.imgur.com/sCScplr.png)


api/users - GET
---------------
![SwaggerOverzicht](https://i.imgur.com/sMaQf44.png)

![SwaggerOverzicht](https://i.imgur.com/P9sEHyE.png)


api/users - POST
----------------
![SwaggerOverzicht](https://i.imgur.com/g8sR8KB.png)

![SwaggerOverzicht](https://i.imgur.com/KRLoU0A.png)

![SwaggerOverzicht](https://i.imgur.com/9RxykYq.png)


api/users/id - GET
------------------
![SwaggerOverzicht](https://i.imgur.com/tEnkGq0.png)

![SwaggerOverzicht](https://i.imgur.com/9ncdyLS.png)

![SwaggerOverzicht](https://i.imgur.com/o5i6FLd.png)

api/users/id - PUT
------------------
![SwaggerOverzicht](https://i.imgur.com/s27vHjU.png)

![SwaggerOverzicht](https://i.imgur.com/O6pe9IF.png)

![SwaggerOverzicht](https://i.imgur.com/q9dToHh.png)


api/users/id - DELETE
---------------------
![SwaggerOverzicht](https://i.imgur.com/RyFXNGN.png)

![SwaggerOverzicht](https://i.imgur.com/18OWbnA.png)

![SwaggerOverzicht](https://i.imgur.com/O7yMotj.png)

