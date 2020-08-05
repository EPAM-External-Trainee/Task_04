# EPAM_External_Trainee_Task_04_Konstantin_Kapatkov
## Task04. Delegates_Events
### Task
* Develop a class hierarchy for implementing client-server interactions using the TCP / IP Protocol stack.
* When the message is received over the network (from the client on the server, or from server on clients) generate an event and pass this message any type for processing that subscribes to this event.
* Types that handle an event on the client must recode message: write a message written in Russian letters to transliterate and by contrast, a message written in Latin characters, present in Russian letters.
* Types that handle an event on the server must maintain lists messages from each client.
* Provide the ability to subscribe to an event in several classes.
* For event handlers, use: 
1. anonymous method;
2. lambda expression.
* Develop unit tests for testing the generated classes.
