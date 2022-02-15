#include "WiFi.h"

const char* password = "s64nb3v48kcc3";

WiFiServer server(80);

void setup()
{
  Serial.begin(115200);
  while (!Serial);
  
  WiFi.begin("Buffalo-G-DAA0", password);	//2.4kHz のみなので注意
  Serial.print("WiFi connecting");
  
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(100);
  }

  Serial.println("connected");

  server.begin();

  Serial.print("HTTP Server: http://");
  Serial.print(WiFi.localIP());
  Serial.println("/");
}


void loop()
{
  WiFiClient client = server.available(); 
  if(client){
    Serial.println("new client"); 
    while(client.connected()){ 
      if(client.available()){ 
        delay(1000);
      }
    }
  }
}


