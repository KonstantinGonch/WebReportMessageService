"""
This script runs the FlaskModelDeploy application using a development server.
"""

from os import environ
from FlaskModelDeploy import app

if __name__ == '__main__':
    HOST = environ.get('SERVER_HOST', 'localhost')
    try:
        PORT = 5555
    except ValueError:
        PORT = 5555
    app.run(HOST, PORT)
