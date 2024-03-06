Bonjour, désolé si vous êtes en train de lire cela et de voir ce code.

Une bonne partie est juste des expérimentations sales pour voir comment cela réagit.
(Par exemple: DragZone1 et DragZone2 étaient pour testé le fait de faire glisser un rectangle d'une zone à une autre.)

La partie haute à servi à tester les liens Vue-'Classe partiel', avec un boutton qui envoie un message en fonction d'une checkbox et d'une textbox.

Les objets rajoutés dans le Canvas sont draggable, et pouvent être lié 2 à 2 avec un click droit.
(Les rectangles sélectionnés sont rouges, si 2 rectangles sont déjà lié, on ne peut pas les reliés.)
(Maximum 1 lien entre 2 objets, le model prend en compte la direction, mais pas le code ou l'affichage).
Lors de la création d'un lien une popup vient demander des informations qui seront stocké sur le lien.
(Ici, uniquement un string d'une liste arbitraire)
Les liens sont redessiné durant le 'draggage' des objets, donc les liens relient toujours les bons objets.

Il y a actuellement que des Rectangles qui sont draggable et reliable.
Mais on peut facilement rajouter d'autres formes en héritant de la classe ObjectLinkableModel.