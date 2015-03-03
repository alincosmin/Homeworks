
#include <stdlib.h>
#include <stdio.h>
#include <math.h>

#include "glut.h"

// dimensiunea ferestrei in pixeli
#define dim 300

unsigned char prevKey;

// concoida lui Nicomede (concoida dreptei)
// $x = a + b \cdot cos(t), y = a \cdot tg(t) + b \cdot sin(t)$. sau
// $x = a - b \cdot cos(t), y = a \cdot tg(t) - b \cdot sin(t)$. unde
// $t \in (-\pi / 2, \pi / 2)$
void Display1() {
	double xmax, ymax, xmin, ymin;
	double a = 1, b = 2;
	double pi = 4 * atan(1.0);
	double ratia = 0.05;
	double t;

	// calculul valorilor maxime/minime ptr. x si y
	// aceste valori vor fi folosite ulterior la scalare
	xmax = a - b - 1;
	xmin = a + b + 1;
	ymax = ymin = 0;
	for (t = -pi / 2 + ratia; t < pi / 2; t += ratia) {
		double x1, y1, x2, y2;
		x1 = a + b * cos(t);
		xmax = (xmax < x1) ? x1 : xmax;
		xmin = (xmin > x1) ? x1 : xmin;

		x2 = a - b * cos(t);
		xmax = (xmax < x2) ? x2 : xmax;
		xmin = (xmin > x2) ? x2 : xmin;

		y1 = a * tan(t) + b * sin(t);
		ymax = (ymax < y1) ? y1 : ymax;
		ymin = (ymin > y1) ? y1 : ymin;

		y2 = a * tan(t) - b * sin(t);
		ymax = (ymax < y2) ? y2 : ymax;
		ymin = (ymin > y2) ? y2 : ymin;
	}

	xmax = (fabs(xmax) > fabs(xmin)) ? fabs(xmax) : fabs(xmin);
	ymax = (fabs(ymax) > fabs(ymin)) ? fabs(ymax) : fabs(ymin);

	// afisarea punctelor propriu-zise precedata de scalare
	glColor3f(1, 0.1, 0.1); // rosu
	glBegin(GL_LINE_STRIP);
	for (t = -pi / 2 + ratia; t < pi / 2; t += ratia) {
		double x1, y1, x2, y2;
		x1 = (a + b * cos(t)) / xmax;
		x2 = (a - b * cos(t)) / xmax;
		y1 = (a * tan(t) + b * sin(t)) / ymax;
		y2 = (a * tan(t) - b * sin(t)) / ymax;

		glVertex2f(x1, y1);
	}
	glEnd();

	glBegin(GL_LINE_STRIP);
	for (t = -pi / 2 + ratia; t < pi / 2; t += ratia) {
		double x1, y1, x2, y2;
		x1 = (a + b * cos(t)) / xmax;
		x2 = (a - b * cos(t)) / xmax;
		y1 = (a * tan(t) + b * sin(t)) / ymax;
		y2 = (a * tan(t) - b * sin(t)) / ymax;

		glVertex2f(x2, y2);
	}
	glEnd();
}

// graficul functiei 
// $f(x) = \bar sin(x) \bar \cdot e^{-sin(x)}, x \in \langle 0, 8 \cdot \pi \rangle$, 
void Display2() {
	double pi = 4 * atan(1.0);
	double xmax = 8 * pi;
	double ymax = exp(1.1);
	double ratia = 0.05;

	// afisarea punctelor propriu-zise precedata de scalare
	glColor3f(1, 0.1, 0.1); // rosu
	glBegin(GL_LINE_STRIP);
	for (double x = 0; x < xmax; x += ratia) {
		double x1, y1;
		x1 = x / xmax;
		y1 = (fabs(sin(x)) * exp(-sin(x))) / ymax;

		glVertex2f(x1, y1);
	}
	glEnd();
}




void Display3()
{
	double xmax = 100;
	double ratia = 0.5;

	glColor3f(1, 0.1, 0.1);
	glBegin(GL_LINE_STRIP);

	double x = 0, y = 1;
	glVertex2f(x, y);

	for (x = ratia ; x <= xmax; x += 1 + ratia)
	{ 
		double dist_r, dist_l;
		dist_r = ceil(x) - x;
		dist_l = x - floor(x);
		
		y = (dist_r > dist_l ? dist_l : dist_r) / x;

		glVertex2f(x/xmax, y);
	}

	glEnd();
}

void Display4(double a, double b)
{
	double ratia = 0.05;
	double pi = 4 * atan(1.0);

	glColor3f(1, 0.1, 0.1);
	glBegin(GL_LINE_STRIP);
	
	for (double t = -pi + ratia; t < pi; t += ratia)
	{
		double x = 2 * (a * cos(t) + b) * cos(t);
		double y = 2 * (a * cos(t) + b) * sin(t);

		glVertex2f(x, y);
	}

	glEnd();
}

struct punct{
	double x;
	double y;
};

void DrawTriangle(struct punct a, struct punct b, struct punct c)
{
	glColor3f(1, 0.1, 0.1);
	glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
	glBegin(GL_TRIANGLES);

	glVertex2f(a.x, a.y);
	glVertex2f(b.x, b.y);
	glVertex2f(c.x, c.y);

	glEnd();
}

void Display5(double a)
{
	double ratia = 0.05;
	double pi = 4 * atan(1.0);

	glColor3f(1, 0.1, 0.1);
	glBegin(GL_LINE_STRIP);

	struct punct p1,p2,p3;

	double t;
	for (t = -pi/2 + ratia; t < -pi/6; t += ratia)
	{
		double x = a / (4 * pow(cos(t), 2) - 3);
		double y = (a * tan(t)) / (4 * pow(cos(t), 2) - 3);

		printf("x:%2f\n", x);

		glVertex2f(x, y);
	}

	glEnd();
}

void Display6(double a, double b)
{
	double ratia = 0.05;
	double pi = 4 * atan(1.0);

	glColor3f(1, 0.1, 0.1);
	glBegin(GL_LINE_STRIP);

	double t;
	for (t = -100; t < 100; t += ratia)
	{
		double x = a * t - b * sin(t);
		double y = a - b*cos(t);

		glVertex2f(x, y);
	}

	glEnd();
}

void Display7(double r1, double r2)
{
	double ratia = 0.05;
	double pi = 4 * atan(1.0);

	glColor3f(1, 0.1, 0.1);
	glBegin(GL_LINE_STRIP);

	double t;
	for (t = 0; t <= 2*pi; t += ratia)
	{
		double x = (r1 + r2)*cos(r2 / r1 * t) - r2*cos(t + r2 / r1 * t);
		double y = (r1 + r2)*sin(r2 / r1 * t) - r2*sin(t + r2 / r1 * t);

		glVertex2f(x, y);
	}

	glEnd();
}

void Display8(double r1, double r2)
{
	double ratia = 0.05;
	double pi = 4 * atan(1.0);

	glColor3f(1, 0.1, 0.1);
	glBegin(GL_LINE_STRIP);

	double t;
	for (t = 0; t <= 2 * pi; t += ratia)
	{
		double x = (r1 - r2)*cos(r2 / r1 * t) - r2*cos(t - r2 / r1 * t);
		double y = (r1 - r2)*sin(r2 / r1 * t) - r2*sin(t - r2 / r1 * t);

		glVertex2f(x, y);
	}

	glEnd();
}

void Display9(double a)
{
	double ratia = 0.01;
	double pi = 4 * atan(1.0);

	glColor3f(1, 0.1, 0.1);
	glBegin(GL_LINE_STRIP);

	double t, x, y, r, x2, y2;
	for (t = -pi/4 + ratia; t <= pi/4; t += ratia)
	{
		r = a * sqrt(2 * cos(2 * t));
		x = r * cos(t);
		y = r * sin(t);

		glVertex2f(x, y);
	}

	glEnd();
	
	glBegin(GL_LINE_STRIP);

	t = -pi / 4 + ratia;
	r = a * sqrt(2 * cos(2 * t));
	x = r * cos(t);
	y = r * sin(t);

	glVertex2f(x, y);
	glVertex2f(-x, -y);


	glEnd();
	
	glBegin(GL_LINE_STRIP);
	x2 = x;
	y2 = y;

	for (t = -pi / 4 + ratia; t <= pi / 4; t += ratia)
	{
		r = -a * sqrt(2 * cos(2 * t));
		x = r * cos(t);
		y = r * sin(t);
		glVertex2f(x, y);
	}

	glEnd();
	
}

void Display10(double a)
{
	double ratia = 0.01;
	double e = 2.71828;

	glColor3f(1, 0.1, 0.1);
	glBegin(GL_LINE_STRIP);

	double t, x, y, r;
	for (t = 0; t <10000; t+= ratia)
	{
		r = a * pow(e, 1+t);
		x = r * cos(t);
		y = r * sin(t);

		glVertex2f(x, y);
	}

	glEnd();

}

void Init(void) {

	glClearColor(1.0, 1.0, 1.0, 1.0);

	glLineWidth(1);

	//   glPointSize(4);

	glPolygonMode(GL_FRONT, GL_LINE);
}

void Display(void) {
	glClear(GL_COLOR_BUFFER_BIT);

	switch (prevKey) {
	case '1':
		Display1();
		break;
	case '2':
		Display2();
		break;

	case '3':
		Display3();
		break;

	case '4':
		Display4(0.3, 0.2);
		break;
	case '5':
		Display5(0.2);
		break;
	case '6':
		Display6(0.1, 0.2);
		break;
	case '7':
		Display7(0.1, 0.3);
		break;
	case '8':
		Display8(0.1, 0.3);
		break;
	case '9':
		Display9(0.4);
		break;
	case '0':
		Display10(0.02);
		break;

	default:
		break;
	}

	glFlush();
}

void Reshape(int w, int h) {
	glViewport(0, 0, (GLsizei)w, (GLsizei)h);
}

void KeyboardFunc(unsigned char key, int x, int y) {
	prevKey = key;
	if (key == 27) // escape
		exit(0);
	glutPostRedisplay();
}

void MouseFunc(int button, int state, int x, int y) {
}

int main(int argc, char** argv) {

	glutInit(&argc, argv);

	glutInitWindowSize(dim, dim);

	glutInitWindowPosition(100, 100);

	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);

	glutCreateWindow(argv[0]);

	Init();

	glutReshapeFunc(Reshape);

	glutKeyboardFunc(KeyboardFunc);

	glutMouseFunc(MouseFunc);

	glutDisplayFunc(Display);

	glutMainLoop();

	return 0;
}
