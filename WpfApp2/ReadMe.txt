Bonjour, d�sol� si vous �tes en train de lire cela et de voir ce code.

Une bonne partie est juste des exp�rimentations sales pour voir comment cela r�agit.
(Par exemple: DragZone1 et DragZone2 �taient pour test� le fait de faire glisser un rectangle d'une zone � une autre.)

La partie haute � servi � tester les liens Vue-'Classe partiel', avec un boutton qui envoie un message en fonction d'une checkbox et d'une textbox.

Les objets rajout�s dans le Canvas sont draggable, et pouvent �tre li� 2 � 2 avec un click droit.
(Les rectangles s�lectionn�s sont rouges, si 2 rectangles sont d�j� li�, on ne peut pas les reli�s.)
(Maximum 1 lien entre 2 objets, le model prend en compte la direction, mais pas le code ou l'affichage).
Lors de la cr�ation d'un lien une popup vient demander des informations qui seront stock� sur le lien.
(Ici, uniquement un string d'une liste arbitraire)
Les liens sont redessin� durant le 'draggage' des objets, donc les liens relient toujours les bons objets.

Il y a actuellement que des Rectangles qui sont draggable et reliable.
Mais on peut facilement rajouter d'autres formes en h�ritant de la classe ObjectLinkableModel.