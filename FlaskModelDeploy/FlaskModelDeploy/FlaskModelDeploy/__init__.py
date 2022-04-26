"""
The flask application package.
"""

from flask import Flask
app = Flask(__name__)

import FlaskModelDeploy.views
from sklearn.feature_extraction.text import CountVectorizer
from flask import request, jsonify
import joblib

loaded_model = joblib.load('model.pkl', 'r+')

def ValuePredictor(message):
    vect = joblib.load('vect.pkl', "r+")
    messageData = {"content" : message}
    X_m = vect.transform(messageData)

    result = loaded_model.predict(X_m)
    return result[0]

@app.route('/predict', methods = ['POST'])
def result():
    if request.method == 'POST':  
        data = request.get_json()
        result = ValuePredictor(data['Content'])  
        return jsonify({'result' : int(result)})
