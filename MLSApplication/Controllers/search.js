public ActionResult LoadDocument(HttpPostedFileBase file) {
    try {
        Medicine newMedicine = new Medicine();
        streamReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory+ "/TestDocuments/" +file.FileName);
        int iteration = 0;
        String data;
        while (streamReader.Peek() >= 0) {
            String lineReader = streamReader.ReadLine();
            data = "";
            for (int i = 0; i < lineReader.Length; i++){
                if (lineReader.Substring(i, 1) != ";"){
                    data = data + lineReader.Substring(i,1);
                } else {
                    if (iteration == 0){
                        if(data != "name"){
                            newMedicine.setName(data);
                        }
                        iteration++;
                        data = "";
                    }else if (iteration == 1){
                        if (data != "ubication"){
                            newMedicine.setUbication(Convert.ToInt(data));
                        }
                        iteration++;
                        data = "";
                    }else if (iteration == 2){
                        if (data != "stocks"){
                            newMedicine.setStocks(Convert.ToInt(data));
                        }
                        iteration++;
                        data = "";
                    }
                }
            }
            if (data != "price){
                newMedicine.setPrice(Convert.ToDouble(data));
            }
            data = "";
            if (newMedicine.name != null) {
                if (newMedicine.saveSportman(Storage.Instance.selectionList)){
                }else{
                    return View(sportsman);
                }
                    
            }
            
            sportsman = new Sportsman();
            iteration = 0;
        }
        streamReader.Close();
        return RedirectToAction("Index");
    }
    catch (Exception e){
        e.ToString();
        return RedirectToAction("Index");
    }
}
}