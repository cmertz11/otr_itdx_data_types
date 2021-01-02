# otr_itdx_data_types
Microservice to host/cache ITDX Data types

## Prerequisites
  
Docker

Microsoft Visual Studio Community 2019 Preview Version 16.9.0 Preview 2.0 or newer.

Data_Types_2020.xsd file (not included in the repo) In the 2020 ITDX xsd zip package.

You find this file at http://www.traumavendoralliance.org/resources/

![](Images/itdsschemadefinition.JPG)

Unzip package and find file Data_Types_2020.xsd.  You need this file to hydrate microservice cache.

## Directions
1. Clone or download
2. Run Docker compose in VS 2019
![](Images/vsrun.JPG)

3. Navigate to http://localhost:8000/swagger/index.html and select POST then click the "Try it out" button.

![](Images/Swagger1.JPG)
![](Images/loadfile.JPG)
