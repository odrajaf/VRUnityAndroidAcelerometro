# VRUnityAndroidAcelerometro

[imagen]:https://github.com/odrajaf/TutorialesPDM/blob/master/images/vr1.png

Este proyecto se muestra como una alternativa a todas esas personas que desean comenzar un proyecto de VR usando el acelerometro 
en lugar del giroscopio.

Se ha implementado un sistema de waypoints basandose en un script creado por jeikobo.
Se han introducido algunos modelos de casas procedentes de https://free3d.com/3d-models/unity .
El modelo como personaje principal ha sido generado mediante el software makehumans.
Las animaciones del personaje principal son las disponibles en el paquete Moka Animation ,sincronizadas con mecanim.
Los modelos de hierba usada son los que vienen con el paquete de asset de unity.

Una vez aclaradas la procedencia de algunos recursos, mostraremos el resultado esteriocopico de la aplicación:

![alt text][imagen]

El efecto se consigue con la configuración de dos camaras a media pantalla.

Además de los configuraciones típicas de Unity algo a destacar sería el script usado en las ambas camaras para simular el moviento
de la cabeza, que podeis encontrar en https://github.com/odrajaf/VRUnityAndroidAcelerometro/blob/master/Assets/camaraAcelerometro.cs

Las ventajas de realizar de esta manera es que aumenta su portabilidad a otras plataformas sin tener que usar una SDK completa, solo se debería modificar el script de las camaras para el dispositivo de tracking que se necesite.
