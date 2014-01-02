#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_ButtonCreate_clicked()
{
    QTextCursor cursor(this->ui->TextboxDocContent->textCursor());
    cursor.insertText("x");
}

void MainWindow::on_ButtonGetDoc_clicked()
{
    this->ui->TextboxDocContent->insertPlainText(this->ui->InputDocName->text());
    this->ui->TextboxDocContent->setReadOnly(true);
}
