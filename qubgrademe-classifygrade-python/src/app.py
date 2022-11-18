from fastapi import FastAPI, Response, status
from fastapi.middleware.cors import CORSMiddleware
from classify import classify
from overall_mark import overall_mark, marks_to_int
from validation import validate
from datetime import datetime
import time
import os

app = FastAPI()

origins = ["*"]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)





start_time = time.time()
port = int(os.environ.get('PORT')) if os.environ.get('PORT') is not None else 9004

@app.get("/", status_code=200)
async def sum(response: Response, module_1: str = "", module_2: str="" , module_3: str="" , module_4: str="" , module_5: str="" , mark_1: str="" , mark_2: str="" , mark_3: str="" , mark_4: str="" , mark_5: str=""):
    modules = [module_1, module_2, module_3, module_4, module_5]
    marks = [mark_1, mark_2, mark_3, mark_4, mark_5]

    res = {
        "error": False,
        "errorMessage" : "",
        "modules": modules,
        "marks": marks,
        "grade" : ""
    }

    success, message = validate(modules, marks)

    if success:
        #  Getting the grade
        overall = overall_mark(marks)
        classification = classify(overall)
        res["grade"] = classification.value
    else:
        res["errorMessage"] = message
        res["error"] = True

    # Removing null values from marks and modules before sending it back
    res["marks"] = list(marks_to_int(marks))
    res["modules"] = list(map(lambda x: "" if x is None else x, modules))


    response.status_code = 200 if success else 400
    return res

@app.get("/health", status_code=200)
async def health():
    data = {
        "date": datetime.now().strftime("%Y-%m-%d %H:%M:%S.%f"),
        "message": "Ok",
        "uptime": time.time() - start_time

    }
    return data

# if __name__ == "__main__":
#     app.run(port=port, host="0.0.0.0")