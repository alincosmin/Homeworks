package cloudhw3

import (
	"appengine"
	"appengine/urlfetch"
	"code.google.com/p/google-api-go-client/plus/v1"
	"fmt"
	"google.golang.org/api/googleapi/transport"
	"html/template"
	"net/http"
)

type PageInfo struct {
	Welcome string
	Images  []string
	EndPage string
}

func init() {
	http.HandleFunc("/cloud", cloudhw3)
}

func cloudhw3(w http.ResponseWriter, r *http.Request) {
	element := PageInfo{
		Welcome: "Hello from /cloud",
		Images:  make([]string, 10),
		EndPage: "End of page :)"}

	t, _ := template.ParseFiles("page.html")

	c := appengine.NewContext(r)

	transport := &transport.APIKey{
		Key:       apiKey,
		Transport: &urlfetch.Transport{Context: c}}

	client := &http.Client{Transport: transport}

	service, err := plus.New(client)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	result, err := service.Activities.Search("#tiger").Do()
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	imageCount := 0
	for _, item := range result.Items {
		for _, att := range item.Object.Attachments {
			if att.ObjectType == "photo" {
				if imageCount < 10 {
					element.Images[imageCount] = att.Image.Url
					imageCount += 1
				}
			}
		}
	}

	t.Execute(w, element)
}

func insertImage(url string) string {
	retElement := fmt.Sprintf("<img style='width: 150px; height: 100px' src='%s' />", url)
	return retElement
}
