// Fonction appelée par onclick des boutons radio du html.
function ChargerInfo(radio) {
  // On récupère la valeur du radio (ex: "chat", "fleuve", "lune", "perso")
    var code = radio.value;

  // get type de lecture (json ou xml)
  var type = document.getElementById('typefichier').value;

  if (type === 'json') {
    chargerJSON(code);
  } else if (type === 'xml') {
    chargerXML(code); 
  }
  chargerDescription(code);
}

// Requête AJAX (XMLHttpRequest) pour lire peintures.json
function chargerJSON(code) {
  var xhr = new XMLHttpRequest();
  xhr.open('GET', 'ajax/peintures.json', true);

  xhr.onreadystatechange = function() {
    if (xhr.readyState === 4) {
      if (xhr.status === 200) {
        //Si reponse(4) finie et OK(200)
        var objet = JSON.parse(xhr.responseText);

        //pour chaque peinture dnas Peintures
        for (var i = 0; i < objet.peintures.length; i++) {
          if (objet.peintures[i].code === code) {
            var p = objet.peintures[i];
            //associer l'array peinture aux propriétés de l'obj json parsed
            var peinture = {
              titre: p.titre,
              artiste: p.artiste,
              prix: p.prix,
              image: p.image || 'img/defaut.jpg'
            };
            afficherPeinture(peinture);//Show peinture generic (json ou xml)
            return;
          }
        }
        console.warn('Peinture non trouvée dans JSON pour le code :', code);
      } else {
        console.error('Erreur lors du chargement JSON, status = ' + xhr.status);
      }
    }
  };

  xhr.send();
}


function chargerXML(code) {
  var xhr = new XMLHttpRequest();
  xhr.open('GET', 'ajax/peintures.xml', true);

  xhr.onreadystatechange = function() {
    if (xhr.readyState === 4) {
      if (xhr.status === 200) {
        //Si reponse(4) finie et OK(200)
        var xmlDoc = xhr.responseXML;
        var peintures = xmlDoc.getElementsByTagName('peinture');

        //pour chaque peinture dnas Peintures
        for (var i = 0; i < peintures.length; i++) {
          var p = peintures[i];
          var codeXML = p.getElementsByTagName('code')[0].textContent;

          //associer les elements de peinture à l'obj xml
          if (codeXML === code) {
            var peinture = {
              titre: p.getElementsByTagName('titre')[0].textContent,
              artiste: p.getElementsByTagName('artiste')[0].textContent,
              prix: p.getElementsByTagName('prix')[0].textContent,
              image: 'img/' + p.getElementsByTagName('image')[0].textContent || 'img/defaut.jpg'
            };
            afficherPeinture(peinture); //Show peinture generic (json ou xml)
            return;
          }
        }
        console.warn('Peinture non trouvée dans XML pour le code :', code);
      } else {
        console.error('Erreur lors du chargement XML, status = ' + xhr.status);
      }
    }
  };

  xhr.send();
}

function chargerDescription(code) {
  var xhr = new XMLHttpRequest();

  // Le fichier texte correspond au code, ex : chat.txt
  var fichierTxt = 'ajax/' + code + '.txt';

  xhr.open('GET', fichierTxt, true);

  xhr.onreadystatechange = function() {
    if (xhr.readyState === 4) {
      if (xhr.status === 200) {

        // Trouver <span id="info">
        var spanInfo = document.getElementById('info');

        // Remove ancien contenu 
        while (spanInfo.firstChild) {spanInfo.removeChild(spanInfo.firstChild);}

        // Ajouter txt reçu
        spanInfo.appendChild(document.createTextNode(xhr.responseText));
      } 
      else {console.error('Erreur lors du chargement du fichier texte:', xhr.status);}
    }
  };
  xhr.send();
}


// Afgfiche peinture en chargeant  avec DOM 
function afficherPeinture(peinture) {

  // Get éléments DOM
  var spanTitre = document.getElementById('titre');
  var spanArtiste = document.getElementById('artiste');
  var spanPrix = document.getElementById('prix');
  var imgElem = document.getElementById('peinture');

  // Vider le contenu existant
  while (spanTitre.firstChild) { spanTitre.removeChild(spanTitre.firstChild); }
  while (spanArtiste.firstChild) { spanArtiste.removeChild(spanArtiste.firstChild); }
  while (spanPrix.firstChild) { spanPrix.removeChild(spanPrix.firstChild); }

  // Append avec createTextNode
  spanTitre.appendChild(document.createTextNode(peinture.titre || ''));
  spanArtiste.appendChild(document.createTextNode(peinture.artiste || ''));
  spanPrix.appendChild(document.createTextNode(peinture.prix || ''));

  // update img
  imgElem.setAttribute('src', peinture.image || 'img/defaut.jpg');
  imgElem.setAttribute('alt', peinture.titre || '');
}
