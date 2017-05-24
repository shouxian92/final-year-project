import time
from coapthon import defines

from coapthon.client.helperclient import HelperClient
from coapthon.utils import parse_uri
from coapthon.resources.resource import Resource
from grovepi import *
from grove_rgb_lcd import *

dht_sensor_port = 7		# Connect the DHt sensor to port 7
dht_sensor_type = 0             # change this depending on your sensor type - see header comment

prevTemp = 0

__author__ = 'Giacomo Tanganelli'

class DisplayResource(Resource):
    def __init__(self, name="DisplayResource", coap_server=None):
        super(DisplayResource, self).__init__(name, coap_server, visible=True,
                                            observable=True, allow_children=True)
        self.payload = "Display Resource"
        self.resource_type = "rt1"
        self.content_type = "text/plain"
        self.interface_type = "if1"

        matric_number = "U12345678A"

        try:
                setText("Display Resource Init")
                setRGB(0,128,64)
		setRGB(0,255,0)
	except (IOError,TypeError) as e:
		print("Error.")

    def render_GET(self, request):
        self.payload = matric_number
        return self

    def render_PUT(self, request):
        self.edit_resource(request)
        return self

    def render_POST(self, request):
        # update text on LCD
        setRGB(0,0,255)
        setText(request.payload)
        setRGB(0,255,0)

        '''
        host, port, path = parse_uri(path)
        client = HelperClient(server=(host, port))        
        response = client.post(path, request.payload)
        print response.pretty_print()
        client.stop()
        '''
        
        return self

    def render_DELETE(self, request):
        return True

green_led = 3
blue_led = 2

class GreenLEDResource(Resource):
    def __init__(self, name="GreenLEDResource", coap_server=None):
        super(GreenLEDResource, self).__init__(name, coap_server, visible=True,
                                            observable=True, allow_children=True)
        self.payload = "Green LED Resource"
        self.resource_type = "rt1"
        self.content_type = "text/plain"
        self.interface_type = "if1"

        try:
            # turn green_led on and off to signify that the resource has successfully been created
            digitalWrite(green_led,1)
            time.sleep(1)
            digitalWrite(green_led,0)
        except (IOError,TypeError) as e:
		print("Green LED Error")
            
    def render_PUT(self, request):
        digitalWrite(green_led, int(request.payload))
        time.sleep(5)
        digitalWrite(green_led,0)
        return self

class BlueLEDResource(Resource):
    def __init__(self, name="BlueEDResource", coap_server=None):
        super(BlueLEDResource, self).__init__(name, coap_server, visible=True,
                                            observable=True, allow_children=True)
        self.payload = "Blue LED Resource"
        self.resource_type = "rt1"
        self.content_type = "text/plain"
        self.interface_type = "if1"

        try:
            # turn green_led on and off to signify that the resource has successfully been created
            digitalWrite(blue_led,1)
            time.sleep(1)
            digitalWrite(blue_led,0)
        except (IOError,TypeError) as e:
		print("BLUE LED Error")
            
    def render_PUT(self, request):
        digitalWrite(blue_led, int(request.payload))
        time.sleep(5)
        digitalWrite(blue_led,0)
        
        return self
            

class BasicResource(Resource):
    def __init__(self, name="BasicResource", coap_server=None):
        super(BasicResource, self).__init__(name, coap_server, visible=True,
                                            observable=True, allow_children=True)
        self.payload = "Basic Resource"
        self.resource_type = "rt1"
        self.content_type = "text/plain"
        self.interface_type = "if1"
        
        prevTemp = 0
        
	try:
                setText("HELLO?")
                '''
		[ temp,hum ] = dht(dht_sensor_port,dht_sensor_type)		#Get the temperature and Humidity from the DHT sensor
		print("temp =", temp, "C\thumidity =", hum,"%") 	
		
		t = str(temp)
		h = str(hum)
		
		setRGB(0,128,64)
		setRGB(0,255,0)
		
		if(temp != prevTemp):
			setText("Temp:" + t + "C      " + "Humidity :" + h + "%")			
			prevTemp = temp
		'''
	except (IOError,TypeError) as e:
		print("Error")
		
    def render_GET(self, request):
        [ temp,hum ] = dht(dht_sensor_port,dht_sensor_type)
        t = str(temp)
        h = str(hum)
        
        setRGB(0,128,64)
        setRGB(0,255,0)

        returnTxt = "Temp:" + t + "C      " + "Humidity :" + h + "%"
        if(temp != prevTemp):
            setText(returnTxt)

        self.payload = returnTxt
                
        return self

    def render_PUT(self, request):
        self.edit_resource(request)
        return self

    def render_POST(self, request):
        # res = self.init_resource(request, BasicResource())

        # update text on LCD
        setText(request.payload)


        # path = "coap://192.168.1.8:5683/lookup-matric";

        host, port, path = parse_uri(path)
        client = HelperClient(server=(host, port))        
        response = client.post(path, request.payload)
        print response.pretty_print()
        client.stop()

        #setText("Message sent to " + path + ": " + request.payload);
        
        return self

    def render_DELETE(self, request):
        return True


class Storage(Resource):
    def __init__(self, name="StorageResource", coap_server=None):
        super(Storage, self).__init__(name, coap_server, visible=True, observable=True, allow_children=True)
        self.payload = "Storage Resource for PUT, POST and DELETE"

    def render_GET(self, request):
        return self

    def render_POST(self, request):
        res = self.init_resource(request, BasicResource())
        return res


class Child(Resource):
    def __init__(self, name="ChildResource", coap_server=None):
        super(Child, self).__init__(name, coap_server, visible=True, observable=True, allow_children=True)
        self.payload = ""

    def render_GET(self, request):
        return self

    def render_PUT(self, request):
        self.payload = request.payload
        return self

    def render_POST(self, request):
        res = BasicResource()
        res.location_query = request.uri_query
        res.payload = request.payload
        return res

    def render_DELETE(self, request):
        return True


class Separate(Resource):

    def __init__(self, name="Separate", coap_server=None):
        super(Separate, self).__init__(name, coap_server, visible=True, observable=True, allow_children=True)
        self.payload = "Separate"
        self.max_age = 60

    def render_GET(self, request):
        return self, self.render_GET_separate

    def render_GET_separate(self, request):
        time.sleep(5)
        return self

    def render_POST(self, request):
        return self, self.render_POST_separate

    def render_POST_separate(self, request):
        self.payload = request.payload
        return self

    def render_PUT(self, request):
        return self, self.render_PUT_separate

    def render_PUT_separate(self, request):
        self.payload = request.payload
        return self

    def render_DELETE(self, request):
        return self, self.render_DELETE_separate

    def render_DELETE_separate(self, request):
        return True


class Long(Resource):

    def __init__(self, name="Long", coap_server=None):
        super(Long, self).__init__(name, coap_server, visible=True, observable=True, allow_children=True)
        self.payload = "Long Time"

    def render_GET(self, request):
        time.sleep(10)
        return self


class Big(Resource):

    def __init__(self, name="Big", coap_server=None):
        super(Big, self).__init__(name, coap_server, visible=True, observable=True, allow_children=True)
        self.payload = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras sollicitudin fermentum ornare. " \
                       "Cras accumsan tellus quis dui lacinia eleifend. Proin ultrices rutrum orci vitae luctus. " \
                       "Nullam malesuada pretium elit, at aliquam odio vehicula in. Etiam nec maximus elit. " \
                       "Etiam at erat ac ex ornare feugiat. Curabitur sed malesuada orci, id aliquet nunc. Phasellus " \
                       "nec leo luctus, blandit lorem sit amet, interdum metus. Duis efficitur volutpat magna, ac " \
                       "ultricies nibh aliquet sit amet. Etiam tempor egestas augue in hendrerit. Nunc eget augue " \
                       "ultricies, dignissim lacus et, vulputate dolor. Nulla eros odio, fringilla vel massa ut, " \
                       "facilisis cursus quam. Fusce faucibus lobortis congue. Fusce consectetur porta neque, id " \
                       "sollicitudin velit maximus eu. Sed pharetra leo quam, vel finibus turpis cursus ac. " \
                       "Aenean ac nisi massa. Cras commodo arcu nec ante tristique ullamcorper. Quisque eu hendrerit" \
                       " urna. Cras fringilla eros ut nunc maximus, non porta nisl mollis. Aliquam in rutrum massa." \
                       " Praesent tristique turpis dui, at ultricies lorem fermentum at. Vivamus sit amet ornare neque, " \
                       "a imperdiet nisl. Quisque a iaculis libero, id tempus lacus. Aenean convallis est non justo " \
                       "consectetur, a hendrerit enim consequat. In accumsan ante a egestas luctus. Etiam quis neque " \
                       "nec eros vestibulum faucibus. Nunc viverra ipsum lectus, vel scelerisque dui dictum a. Ut orci " \
                       "enim, ultrices a ultrices nec, pharetra in quam. Donec accumsan sit amet eros eget fermentum." \
                       "Vivamus ut odio ac odio malesuada accumsan. Aenean vehicula diam at tempus ornare. Phasellus " \
                       "dictum mauris a mi consequat, vitae mattis nulla fringilla. Ut laoreet tellus in nisl efficitur," \
                       " a luctus justo tempus. Fusce finibus libero eget velit finibus iaculis. Morbi rhoncus purus " \
                       "vel vestibulum ullamcorper. Sed ac metus in urna fermentum feugiat. Nulla nunc diam, sodales " \
                       "aliquam mi id, varius porta nisl. Praesent vel nibh ac turpis rutrum laoreet at non odio. " \
                       "Phasellus ut posuere mi. Suspendisse malesuada velit nec mauris convallis porta. Vivamus " \
                       "sed ultrices sapien, at cras amet."

    def render_GET(self, request):
        return self

    def render_POST(self, request):
        if request.payload is not None:
            self.payload = request.payload
        return self


class voidResource(Resource):
    def __init__(self, name="Void"):
        super(voidResource, self).__init__(name)


class XMLResource(Resource):
    def __init__(self, name="XML"):
        super(XMLResource, self).__init__(name)
        self.value = 0
        self.payload = (defines.Content_types["application/xml"], "<value>"+str(self.value)+"</value>")

    def render_GET(self, request):
        return self


class MultipleEncodingResource(Resource):
    def __init__(self, name="MultipleEncoding"):
        super(MultipleEncodingResource, self).__init__(name)
        self.value = 0
        self.payload = str(self.value)
        self.content_type = [defines.Content_types["application/xml"], defines.Content_types["application/json"]]

    def render_GET(self, request):
        if request.accept == defines.Content_types["application/xml"]:
            self.payload = (defines.Content_types["application/xml"],  "<value>"+str(self.value)+"</value>")
        elif request.accept == defines.Content_types["application/json"]:
            self.payload = (defines.Content_types["application/json"], "{'value': '"+str(self.value)+"'}")
        elif request.accept == defines.Content_types["text/plain"]:
            self.payload = (defines.Content_types["text/plain"], str(self.value))
        return self

    def render_PUT(self, request):
        self.edit_resource(request)
        return self

    def render_POST(self, request):
        res = self.init_resource(request, MultipleEncodingResource())
        return res


class ETAGResource(Resource):
    def __init__(self, name="ETag"):
        super(ETAGResource, self).__init__(name)
        self.count = 0
        self.payload = "ETag resource"
        self.etag = str(self.count)

    def render_GET(self, request):
        return self

    def render_POST(self, request):
        self.payload = request.payload
        self.count += 1
        self.etag = str(self.count)
        return self

    def render_PUT(self, request):
        self.payload = request.payload
        self.reply_payload = True
        return self
