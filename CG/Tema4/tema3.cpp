
#include <stdlib.h>
#include <stdio.h>
#include <math.h>

#include "glut.h"

unsigned char prevKey;

class GrilaCarteziana{
	int _linii, _coloane;
public:
	int GrilaCarteziana::Linii() {
		return linii;
	}

	int GrilaCarteziana::Coloane() {
		return coloane;
	}

	void GrilaCarteziana :: writePixel(int linie, int coloana, float nr_linii, float nr_coloane){
			const float DEG2RAD = 3.14159/180;
			float grila_linie = 2 / nr_linii;
			float grila_coloana = 2 / nr_coloane;

				glPolygonMode(GL_FRONT, GL_FILL);
				glBegin(GL_POLYGON);
				glColor3f(0.0,0.0,0.4);

					for (int i=0; i < 360; i++)
					{
						float degInRad = i*DEG2RAD;
						glVertex2f(cos(degInRad)*0.05f + (-1 + grila_coloana * coloana),sin(degInRad)*0.04f +(-1 + grila_linie * linie));
					
					}
				glEnd();
	
	}

	void GrilaCarteziana :: creareGrila(float nr_linii, float nr_coloane){

	float grila_linie = 2 / nr_linii;
	float grila_coloana = 2 / nr_coloane;

	glColor3f(0.0,0.0,0.0); 
	glLineWidth(1);
	for (int i=0;i<nr_linii;i++)
	{
		glBegin(GL_LINES);
			glVertex2f(-1,-1+grila_linie*i);
			glVertex2f(1,-1+grila_linie*i);
		glEnd();
	}
	for(int j=0;j< nr_coloane;j++){
		glBegin(GL_LINES);
			glVertex2f(-1+grila_coloana*j,-1); 
			glVertex2f(-1+grila_coloana*j,1);
		glEnd(); 
	}

}
	void swap(int& i, int& j) {
		int t = i;
		i = j;
		j = t;
	}

	void GrilaCarteziana :: desenareDreapta(GrilaCarteziana grila,int x0,int y0, int xn,int yn,float nr_linii, float nr_coloane){

		float grila_linie = 2 / nr_linii;
		float grila_coloana = 2 / nr_coloane;
		int dx,dy,d,dE,dNE,dSE,x,y,dN,dS;
		if (x0 > xn)
		{
			swap(x0, xn);
			swap(y0, yn);
		}

		dx = xn - x0;
		dy = yn - y0;

		x = x0;
		y = y0;
		if (dy >= 0)
		{
			if(abs(dy) <= abs(dx))
			{				
				d = 2 * dy - dx;
				dE = 2 * dy;
				dNE = 2* (dy - dx);
				while(x <= xn)
				{
					grila.writePixel(y,x,nr_linii,nr_coloane);
					if (d <= 0)
					{
						d += dE;
						x++;
					}
					else
					{
						d += dNE;
						x++;
						y++;
					}
				}
			}
			else
			{
				d = dy - 2*dx;
				dN = -2 * dx;
				dNE = 2 * (dy - dx);

				while ( y <= yn)
				{
					grila.writePixel(y,x,nr_linii,nr_coloane);
					if (d > 0)
					{
						d += dN;
						y++;
					}
					else
					{
						d += dNE;
						x++;
						y++;
					}
				}
			}
		}
		else
		{		
			if(abs(dy) <= abs(dx))
			{	
				d = 2 * dy + dx;
				dE = 2 * dy;
				dSE = 2* (dy + dx);

				while(x <= xn)
				{
					grila.writePixel(y,x,nr_linii,nr_coloane);
					if (d > 0)
					{
						d += dE;
						x++;
					}
					else
					{
						d += dSE;
						x++;
						y--;
					}
				}
			}
			else
			{
				d = dy + 2*dx;
				dS = 2 * dx;
				dSE = 2* (dy + dx);

				while ( y >= yn)
				{
					grila.writePixel(y,x,nr_linii,nr_coloane);
					if (d <= 0)
					{
						d += dS;
						y--;
					}
					else
					{
						d += dSE;
						x++;
						y--;
					}
				}
			}
		}
		glLineWidth(5);
		glColor3f(1,0.1,0.1);
		glBegin(GL_LINES); 
			glVertex2f(-1 + grila_coloana * x0,-1 + grila_linie * y0); 
			glVertex2f(-1 + grila_coloana * xn,-1 + grila_linie * yn);
		glEnd();

}

	
	
};

void Display1() {

	GrilaCarteziana grila;
	grila.creareGrila(15,20);
	grila.desenareDreapta(grila,15,0,0,15);
	grila.desenareDreapta(grila, 0, 0, 15, 15);
	grila.desenareDreapta(grila, 18, 12, 18, 3);
	grila.desenareDreapta(grila, 7, 15, 15, 15);
	grila.desenareDreapta(grila, 20, 10, 20, 0);

	grila.desenareDreapta(grila,-15,15,15,-15,15,20);
	
}

void Init(void) {

   glClearColor(1.0,1.0,1.0,1.0);

   glLineWidth(3);

   glPointSize(4);

   glPolygonMode(GL_FRONT, GL_LINE);
}

void Display(void) {
   printf("Call Display\n");

   // sterge buffer-ul indicat
   glClear(GL_COLOR_BUFFER_BIT);

   switch(prevKey) {
   case '1':
      Display1();
      break;
   default:
      break;
   }

   // forteaza redesenarea imaginii
   glFlush();
}

void Reshape(int w, int h) {
   printf("Call Reshape : latime = %d, inaltime = %d\n", w, h);

	glViewport(0, 0, (GLsizei) w, (GLsizei) h);
}

void KeyboardFunc(unsigned char key, int x, int y) {
   printf("Ati tastat <%c>. Mouse-ul este in pozitia %d, %d.\n",
            key, x, y);
   
   prevKey = key;
   if (key == 27) // escape
      exit(0);
   glutPostRedisplay();
}


void MouseFunc(int button, int state, int x, int y) {
   printf("Call MouseFunc : ati %s butonul %s in pozitia %d %d\n",
      (state == GLUT_DOWN) ? "apasat" : "eliberat",
      (button == GLUT_LEFT_BUTTON) ? 
      "stang" : 
      ((button == GLUT_RIGHT_BUTTON) ? "drept": "mijlociu"),
      x, y);
}

int main(int argc, char** argv) {

   glutInit(&argc, argv);
   glutInitWindowSize(300, 300);

  
   glutInitWindowPosition(100, 100);

 
   glutInitDisplayMode (GLUT_SINGLE | GLUT_RGB);

   glutCreateWindow (argv[0]);

   Init();

   glutReshapeFunc(Reshape);
   

   glutKeyboardFunc(KeyboardFunc);
   
   glutMouseFunc(MouseFunc);

   glutDisplayFunc(Display);
   
   glutMainLoop();

   return 0;
}
