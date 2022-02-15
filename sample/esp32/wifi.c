#include <WiFi.h>

const char* ssid     = "NaracoorteCityCouncil";
const char* password = "kaikusai134";

WiFiServer server(80);

void setup()
{
  Serial.begin(115200);

  delay(10);

  Serial.println();
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
      delay(500);
      Serial.print(".");
  }

  Serial.println("");
  Serial.println("WiFi connected.");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
  
  server.begin();
}


int value = 0;

void loop() 
{ 
  //接続したクライアントの情報を得る。 
  WiFiClient client = server.available(); 
  if(client){ 
    Serial.println("new client"); 
    while(client.connected()){ 
      if(client.available()){ 
        value=client.read(); 
        Serial.println(value); 
        delay(1000);      
      }
    }    
  }
}
