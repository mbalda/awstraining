import logging

logger = logging.getLogger()
logger.setLevel(logging.INFO)

def lambda_handler(event, context):

    logger.info("Hello World function")
    logger.info('Event:'.format(event))
    
    name = event['name']
    response = 'Hello World. My name is ' + name + '!'
  
    return response
